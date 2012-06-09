﻿#region CodeMaid is Copyright 2007-2012 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License version 3
// as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2012 Steve Cadwallader.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EnvDTE;
using SteveCadwallader.CodeMaid.CodeItems;
using SteveCadwallader.CodeMaid.Properties;

namespace SteveCadwallader.CodeMaid.Helpers
{
    /// <summary>
    /// A class for encapsulating explicit access modifier behavior.
    /// </summary>
    internal class ExplicitAccessModifierHelper
    {
        #region Constants

        /// <summary>
        /// The string representation of the partial keyword.
        /// </summary>
        private const string PartialKeyword = "partial";

        #endregion Constants

        #region Constructors

        /// <summary>
        /// The singleton instance of the <see cref="ExplicitAccessModifierHelper"/> class.
        /// </summary>
        private static ExplicitAccessModifierHelper _instance;

        /// <summary>
        /// Gets an instance of the <see cref="ExplicitAccessModifierHelper"/> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        /// <returns>An instance of the <see cref="ExplicitAccessModifierHelper"/> class.</returns>
        internal static ExplicitAccessModifierHelper GetInstance(CodeMaidPackage package)
        {
            return _instance ?? (_instance = new ExplicitAccessModifierHelper(package));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplicitAccessModifierHelper"/> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        private ExplicitAccessModifierHelper(CodeMaidPackage package)
        {
            Package = package;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Inserts the explicit access modifiers on classes where they are not specified.
        /// </summary>
        /// <param name="classes">The classes.</param>
        public void InsertExplicitAccessModifiersOnClasses(IEnumerable<CodeItemClass> classes)
        {
            if (!Settings.Default.Cleaning_InsertExplicitAccessModifiersOnClasses) return;

            foreach (var codeClass in classes.Select(x => x.CodeClass).Where(y => y != null))
            {
                var classDeclaration = CodeModelHelper.GetClassDeclaration(codeClass);

                // Skip partial classes - access modifier may be specified elsewhere.
                if (IsKeywordSpecified(classDeclaration, PartialKeyword))
                {
                    continue;
                }

                if (!IsAccessModifierExplicitlySpecifiedOnCodeElement(classDeclaration, codeClass.Access))
                {
                    // Set the access value to itself to cause the code to be added.
                    codeClass.Access = codeClass.Access;
                }
            }
        }

        /// <summary>
        /// Inserts the explicit access modifiers on delegates where they are not specified.
        /// </summary>
        /// <param name="delegates">The delegates.</param>
        public void InsertExplicitAccessModifiersOnDelegates(IEnumerable<CodeItemDelegate> delegates)
        {
            if (!Settings.Default.Cleaning_InsertExplicitAccessModifiersOnDelegates) return;

            foreach (var codeDelegate in delegates.Select(x => x.CodeDelegate).Where(y => y != null))
            {
                var delegateDeclaration = CodeModelHelper.GetDelegateDeclaration(codeDelegate);

                if (!IsAccessModifierExplicitlySpecifiedOnCodeElement(delegateDeclaration, codeDelegate.Access))
                {
                    // Set the access value to itself to cause the code to be added.
                    codeDelegate.Access = codeDelegate.Access;
                }
            }
        }

        /// <summary>
        /// Inserts the explicit access modifiers on enumerations where they are not specified.
        /// </summary>
        /// <param name="enumerations">The enumerations.</param>
        public void InsertExplicitAccessModifiersOnEnumerations(IEnumerable<CodeItemEnum> enumerations)
        {
            if (!Settings.Default.Cleaning_InsertExplicitAccessModifiersOnEnumerations) return;

            foreach (var codeEnum in enumerations.Select(x => x.CodeEnum).Where(y => y != null))
            {
                var enumDeclaration = CodeModelHelper.GetEnumerationDeclaration(codeEnum);

                if (!IsAccessModifierExplicitlySpecifiedOnCodeElement(enumDeclaration, codeEnum.Access))
                {
                    // Set the access value to itself to cause the code to be added.
                    codeEnum.Access = codeEnum.Access;
                }
            }
        }

        /// <summary>
        /// Inserts the explicit access modifiers on events where they are not specified.
        /// </summary>
        /// <param name="events">The events.</param>
        public void InsertExplicitAccessModifiersOnEvents(IEnumerable<CodeItemEvent> events)
        {
            if (!Settings.Default.Cleaning_InsertExplicitAccessModifiersOnEvents) return;

            foreach (var codeEvent in events.Select(x => x.CodeEvent).Where(y => y != null))
            {
                try
                {
                    // Skip events defined inside an interface.
                    if (codeEvent.Parent is CodeInterface)
                    {
                        continue;
                    }

                    // Skip explicit interface implementations.
                    // Name is reported different for CodeEvent - so combine with parent to determine if interface is being explicitly specified.
                    if (codeEvent.Parent is CodeElement &&
                        codeEvent.FullName != (((CodeElement)codeEvent.Parent).FullName + "." + codeEvent.Name))
                    {
                        continue;
                    }
                }
                catch (Exception)
                {
                    // Skip this event if unable to analyze.
                    continue;
                }

                var eventDeclaration = CodeModelHelper.GetEventDeclaration(codeEvent);

                if (!IsAccessModifierExplicitlySpecifiedOnCodeElement(eventDeclaration, codeEvent.Access))
                {
                    // Set the access value to itself to cause the code to be added.
                    codeEvent.Access = codeEvent.Access;
                }
            }
        }

        /// <summary>
        /// Inserts the explicit access modifiers on fields where they are not specified.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public void InsertExplicitAccessModifiersOnFields(IEnumerable<CodeItemField> fields)
        {
            if (!Settings.Default.Cleaning_InsertExplicitAccessModifiersOnFields) return;

            foreach (var codeField in fields.Select(x => x.CodeVariable).Where(y => y != null))
            {
                try
                {
                    // Skip "fields" defined inside an enumeration.
                    if (codeField.Parent is CodeEnum)
                    {
                        continue;
                    }
                }
                catch (Exception)
                {
                    // Skip this field if unable to analyze.
                    continue;
                }

                var fieldDeclaration = CodeModelHelper.GetFieldDeclaration(codeField);

                if (!IsAccessModifierExplicitlySpecifiedOnCodeElement(fieldDeclaration, codeField.Access))
                {
                    // Set the access value to itself to cause the code to be added.
                    codeField.Access = codeField.Access;
                }
            }
        }

        /// <summary>
        /// Inserts the explicit access modifiers on interfaces where they are not specified.
        /// </summary>
        /// <param name="interfaces">The interfaces.</param>
        public void InsertExplicitAccessModifiersOnInterfaces(IEnumerable<CodeItemInterface> interfaces)
        {
            if (!Settings.Default.Cleaning_InsertExplicitAccessModifiersOnInterfaces) return;

            foreach (var codeInterface in interfaces.Select(x => x.CodeInterface).Where(y => y != null))
            {
                var interfaceDeclaration = CodeModelHelper.GetInterfaceDeclaration(codeInterface);

                if (!IsAccessModifierExplicitlySpecifiedOnCodeElement(interfaceDeclaration, codeInterface.Access))
                {
                    // Set the access value to itself to cause the code to be added.
                    codeInterface.Access = codeInterface.Access;
                }
            }
        }

        /// <summary>
        /// Inserts the explicit access modifiers on methods where they are not specified.
        /// </summary>
        /// <param name="methods">The methods.</param>
        public void InsertExplicitAccessModifiersOnMethods(IEnumerable<CodeItemMethod> methods)
        {
            if (!Settings.Default.Cleaning_InsertExplicitAccessModifiersOnMethods) return;

            foreach (var codeFunction in methods.Select(x => x.CodeFunction).Where(y => y != null))
            {
                try
                {
                    // Skip static constructors - they should not have an access modifier.
                    if (codeFunction.IsShared && codeFunction.FunctionKind == vsCMFunction.vsCMFunctionConstructor)
                    {
                        continue;
                    }

                    // Skip destructors - they should not have an access modifier.
                    if (codeFunction.FunctionKind == vsCMFunction.vsCMFunctionDestructor)
                    {
                        continue;
                    }

                    // Skip explicit interface implementations.
                    if (codeFunction.Name.Contains("."))
                    {
                        continue;
                    }

                    // Skip methods defined inside an interface.
                    if (codeFunction.Parent is CodeInterface)
                    {
                        continue;
                    }
                }
                catch (Exception)
                {
                    // Skip this method if unable to analyze.
                    continue;
                }

                var methodDeclaration = CodeModelHelper.GetMethodDeclaration(codeFunction);

                // Skip partial methods - access modifier may be specified elsewhere.
                if (IsKeywordSpecified(methodDeclaration, PartialKeyword))
                {
                    continue;
                }

                if (!IsAccessModifierExplicitlySpecifiedOnCodeElement(methodDeclaration, codeFunction.Access))
                {
                    // Set the access value to itself to cause the code to be added.
                    codeFunction.Access = codeFunction.Access;
                }
            }
        }

        /// <summary>
        /// Inserts the explicit access modifiers on properties where they are not specified.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public void InsertExplicitAccessModifiersOnProperties(IEnumerable<CodeItemProperty> properties)
        {
            if (!Settings.Default.Cleaning_InsertExplicitAccessModifiersOnProperties) return;

            foreach (var codeProperty in properties.Select(x => x.CodeProperty).Where(y => y != null))
            {
                try
                {
                    // Skip explicit interface implementations.
                    if (codeProperty.Name.Contains("."))
                    {
                        continue;
                    }

                    // Skip properties defined inside an interface.
                    if (codeProperty.Parent is CodeInterface)
                    {
                        continue;
                    }
                }
                catch (Exception)
                {
                    // Skip this property if unable to analyze.
                    continue;
                }

                var propertyDeclaration = CodeModelHelper.GetPropertyDeclaration(codeProperty);

                if (!IsAccessModifierExplicitlySpecifiedOnCodeElement(propertyDeclaration, codeProperty.Access))
                {
                    // Set the access value to itself to cause the code to be added.
                    codeProperty.Access = codeProperty.Access;
                }
            }
        }

        /// <summary>
        /// Inserts the explicit access modifiers on structs where they are not specified.
        /// </summary>
        /// <param name="structs">The structs.</param>
        public void InsertExplicitAccessModifiersOnStructs(IEnumerable<CodeItemStruct> structs)
        {
            if (!Settings.Default.Cleaning_InsertExplicitAccessModifiersOnStructs) return;

            foreach (var codeStruct in structs.Select(x => x.CodeStruct).Where(y => y != null))
            {
                var structDeclaration = CodeModelHelper.GetStructDeclaration(codeStruct);

                if (!IsAccessModifierExplicitlySpecifiedOnCodeElement(structDeclaration, codeStruct.Access))
                {
                    // Set the access value to itself to cause the code to be added.
                    codeStruct.Access = codeStruct.Access;
                }
            }
        }

        /// <summary>
        /// Determines if the access modifier is explicitly defined on the specified code element declaration.
        /// </summary>
        /// <param name="codeElementDeclaration">The code element declaration.</param>
        /// <param name="accessModifier">The access modifier.</param>
        /// <returns>True if access modifier is explicitly specified, otherwise false.</returns>
        private static bool IsAccessModifierExplicitlySpecifiedOnCodeElement(string codeElementDeclaration, vsCMAccess accessModifier)
        {
            string keyword = CodeModelHelper.GetAccessModifierKeyword(accessModifier);

            return IsKeywordSpecified(codeElementDeclaration, keyword);
        }

        /// <summary>
        /// Determines if the specified keyword is present in the specified code element declaration.
        /// </summary>
        /// <param name="codeElementDeclaration">The code element declaration.</param>
        /// <param name="keyword">The keyword.</param>
        /// <returns>True if the keyword is present, otherwise false.</returns>
        private static bool IsKeywordSpecified(string codeElementDeclaration, string keyword)
        {
            string matchString = @"(^|\s)" + keyword + @"\s";

            return Regex.IsMatch(codeElementDeclaration, matchString);
        }

        #endregion Methods

        #region Private Properties

        /// <summary>
        /// Gets or sets the hosting package.
        /// </summary>
        private CodeMaidPackage Package { get; set; }

        #endregion Private Properties
    }
}