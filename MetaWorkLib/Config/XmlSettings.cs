using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
namespace MetaWorkLib.Config
{
    public class XmlSettings
    {
        private const string xmlFormatVer = "1.1";

        private XmlDocument doc = new XmlDocument();
        private XmlElement xmlBody;
        private string xmlContents, xmlFileFormatVer, xmlFileFormat, xmlSettingsFileVer;
        private const string xmlSettingsMinSupportedVer = "1.0";
        private XmlDeclaration xmlDeclaration;

        private string mainSettingsPath, errorLogPathLoc, backupPath;
        private bool revertOnFail, purgeOnRootFailure, verboseLogging, verboseLevelMode, purgeIfXmlDecMissing, purgeOnUnsupportedVer, performBackups;
        private bool isXmlCompatible = true;
        private bool lockXML = false;

        /// <summary>
        /// Allows the use of the XmlSettings library.
        /// </summary>
        /// <param name="settingsPath">The path for the settings file, can either be relative or absolute.</param>
        /// <param name="revertToDefaultOnFail">States if the settings should be reverted to the default if it cannot be parsed.</param>
        /// <param name="deleteFileIfRootMissing">States if the XML file should be automatically deleted (and recreated) if the root is missing.</param>
        /// <param name="verbose">States if XMLSettings outputs verbose logging to the log file, rather than just errors.</param>
        /// <param name="enhancedVerbose">The level of verbose logging that is performed (if it is enabled).</param>
        /// <param name="deleteIfXmlDeclarationMissing">States if the XML file should be automatically deleted (and recreated) if the XML Declaration is missing.</param>
        /// <param name="deleteOnUnsupportedVersion">States if the XML file should be automatically deleted (and recreated) if the XmlSettings Encoding version is unsupported and unmigratable.</param>
        /// <param name="skipVersionMigration">States if the XML file should be not be automatically updated to support the latest XmlSettings encoding version.</param>
        /// <param name="logPath">The path for the log file.</param>
        /// <param name="overrideBackupFile">Override the default path for file backups (default path is the settingsPath plus '.bkup').</param>
        public XmlSettings(string settingsPath, bool revertToDefaultOnFail = true, bool deleteFileIfRootMissing = true, bool verbose = false, bool enhancedVerbose = false, bool deleteIfXmlDeclarationMissing = true, bool deleteOnUnsupportedVersion = false, bool alwaysBackupBeforeDeletion = true, string logPath = "XmlSettings.log", string overrideBackupFile = null)
        {
            mainSettingsPath = settingsPath;
            revertOnFail = revertToDefaultOnFail;
            purgeOnRootFailure = deleteFileIfRootMissing;
            errorLogPathLoc = logPath;
            verboseLogging = verbose;
            verboseLevelMode = enhancedVerbose;
            purgeIfXmlDecMissing = deleteIfXmlDeclarationMissing;
            purgeOnUnsupportedVer = deleteOnUnsupportedVersion;
            performBackups = alwaysBackupBeforeDeletion;

            if (String.IsNullOrEmpty(overrideBackupFile))
            {
                backupPath = settingsPath.TrimEnd(new char[] { '\\', '/' }) + ".bkup";
            }
            else
            {
                backupPath = overrideBackupFile;
            }

            if (verboseLogging && !enhancedVerbose)
            {
                writeToLog("New Instance Started with Verbose Logging... Hello World!");
            }
            else if (verboseLogging && enhancedVerbose)
            {
                writeToLog("New Instance Started with Enhanced Verbose Logging... Hello World!");
                writeToLog("settingsPath: " + settingsPath + ", revertOnFail: " + revertOnFail + ", deleteFileIfRootMissing: " + deleteFileIfRootMissing + ", verbose: " + verbose + ", enhancedVerbose: " + enhancedVerbose + ", deleteIfXmlDeclarationMissing: "
               + deleteIfXmlDeclarationMissing + ", deleteOnUnsupportedVersion: " + deleteOnUnsupportedVersion + ", alwaysBackupBeforeDeletion: " + alwaysBackupBeforeDeletion + ", logPath: " + logPath + ", overrideBackupFile: " + overrideBackupFile);
            }
            else
            {
                writeToLog("New Instance Started... Hello World!");
            }

            initXMLSettings();
        }

        /// <summary>
        /// Checks, loads and cleans up the XML file if it already exists. Or creates a new XML file if one doesn't.
        /// </summary>
        private void initXMLSettings()
        {
            if (!File.Exists(mainSettingsPath))
            {
                lockXML = true;
                createXMLFile();
            }
            else
            {
                cleanupXML();
            }

            checkXML();
        }

        /// <summary>
        /// Resets XMLSettings variables to their original values and reloads the XML from scratch.
        /// </summary>
        public void reloadXMLSettings()
        {
            writeToLog("Reloading Instance...");

            doc = new XmlDocument();
            xmlBody = null;
            xmlContents = xmlFileFormatVer = xmlFileFormat = xmlSettingsFileVer = null;
            xmlDeclaration = null;
            isXmlCompatible = true;
            lockXML = false;

            initXMLSettings();
        }

        /// <summary>
        /// Used to get the current version of XmlSettings.
        /// </summary>
        /// <returns>The version number of XmlSettings.</returns>
        public string getVersion()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        }

        /// <summary>
        /// Used to get the current XmlSettings encoding version.
        /// </summary>
        /// <returns>The current XmlSettings encoding version.</returns>
        public string getEncodingVersion()
        {
            return xmlFormatVer;
        }

        /// <summary>
        /// Used to get the XmlSettings encoding version of XML file.
        /// </summary>
        /// <returns>The XmlSettings encoding version of XML file.</returns>
        public string getFileXmlSettingsVersion()
        {
            return xmlSettingsFileVer;
        }

        /// <summary>
        /// Used to get the minimum supported XmlSettings encoding version.
        /// </summary>
        /// <returns>The minimum supported XmlSettings encoding version.</returns>
        public string getMinSupportedEncodingVersion()
        {
            return xmlSettingsMinSupportedVer;
        }

        /// <summary>
        /// Used to get the XML version of the currently loaded file.
        /// </summary>
        /// <returns>The XML version number of the loaded file.</returns>
        public string getXmlVersion()
        {
            return xmlFileFormatVer;
        }

        /// <summary>
        /// Used to get the encoding of the currently loaded file.
        /// </summary>
        /// <returns>The encoding type of the loaded file.</returns>
        public string getFileEncoding()
        {
            return xmlFileFormat;
        }

        /// <summary>
        /// Creates a Boolean variable in the XML file with the specified value.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <param name="defaultValue">The value that will be assigned to the variable. Also used as a 'default' and can be reverted to at a later date.</param>
        /// <returns>If the variables was added successfully.</returns>
        public bool addBoolean(string varName, bool defaultValue)
        {
            if (!doesDuplicateExist(varName) && checkIfXmlCompatible())
            {
                XmlNode booleanNode = addToHeadingNode("boolean");
                xmlBody.AppendChild(booleanNode);

                XmlNode varNode = doc.CreateElement(varName);
                XmlAttribute defaultVal = doc.CreateAttribute("default");
                defaultVal.Value = defaultValue.ToString();
                varNode.Attributes.Append(defaultVal);
                varNode.InnerText = defaultValue.ToString();
                booleanNode.AppendChild(varNode);
                writeToLog("Added Boolean: '" + varName + "' with a value of: '" + defaultValue + "'");
                saveXML();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a String variable in the XML file with the specified value.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <param name="defaultValue">The value that will be assigned to the variable. Also used as a 'default' and can be reverted to at a later date.</param>
        /// <returns>If the variables was added successfully.</returns>
        public bool addString(string varName, string defaultValue)
        {
            if (!doesDuplicateExist(varName) && checkIfXmlCompatible())
            {
                XmlNode stringNode = addToHeadingNode("string");
                xmlBody.AppendChild(stringNode);

                XmlNode varNode = doc.CreateElement(varName);
                XmlAttribute defaultVal = doc.CreateAttribute("default");
                defaultVal.Value = defaultValue;
                varNode.Attributes.Append(defaultVal);
                varNode.InnerText = defaultValue;
                stringNode.AppendChild(varNode);
                writeToLog("Added String: '" + varName + "' with a value of: '" + defaultValue + "'");
                saveXML();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a Long variable in the XML file with the specified value.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <param name="defaultValue">The value that will be assigned to the variable. Also used as a 'default' and can be reverted to at a later date.</param>
        /// <returns>If the variables was added successfully.</returns>
        public bool addLong(string varName, long defaultValue)
        {
            if (!doesDuplicateExist(varName) && checkIfXmlCompatible())
            {
                XmlNode longNode = addToHeadingNode("long");
                XmlNode varNode = doc.CreateElement(varName);
                XmlAttribute defaultVal = doc.CreateAttribute("default");
                defaultVal.Value = defaultValue.ToString();
                varNode.Attributes.Append(defaultVal);
                varNode.InnerText = defaultValue.ToString();
                longNode.AppendChild(varNode);
                writeToLog("Added Long: '" + varName + "' with a value of: '" + defaultValue + "'");
                saveXML();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a Int variable in the XML file with the specified value.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <param name="defaultValue">The value that will be assigned to the variable. Also used as a 'default' and can be reverted to at a later date.</param>
        /// <returns>If the variables was added successfully.</returns>
        public bool addInt(string varName, int defaultValue)
        {
            if (!doesDuplicateExist(varName) && checkIfXmlCompatible())
            {
                XmlNode intNode = addToHeadingNode("int");
                XmlNode varNode = doc.CreateElement(varName);
                XmlAttribute defaultVal = doc.CreateAttribute("default");
                defaultVal.Value = defaultValue.ToString();
                varNode.Attributes.Append(defaultVal);
                varNode.InnerText = defaultValue.ToString();
                intNode.AppendChild(varNode);
                writeToLog("Added Int: '" + varName + "' with a value of: '" + defaultValue + "'");
                saveXML();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a Double variable in the XML file with the specified value.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <param name="defaultValue">The value that will be assigned to the variable. Also used as a 'default' and can be reverted to at a later date.</param>
        /// <returns>If the variables was added successfully.</returns>
        public bool addDouble(string varName, double defaultValue)
        {
            if (!doesDuplicateExist(varName) && checkIfXmlCompatible())
            {
                XmlNode doubleNode = addToHeadingNode("double");
                XmlNode varNode = doc.CreateElement(varName);
                XmlAttribute defaultVal = doc.CreateAttribute("default");
                defaultVal.Value = defaultValue.ToString();
                varNode.Attributes.Append(defaultVal);
                varNode.InnerText = defaultValue.ToString();
                doubleNode.AppendChild(varNode);
                writeToLog("Added Double: '" + varName + "' with a value of: '" + defaultValue + "'");
                saveXML();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets the Boolean variable of an already existing variable in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <param name="state">The value that will be assigned to the variable.</param>
        /// <returns>If the variables was set successfully.</returns>
        public bool setBoolean(string varName, bool state)
        {
            if (checkIfXmlCompatible())
            {
                loadXML();
                XmlNodeList elemList = doc.GetElementsByTagName(varName);
                if (elemList.Count == 1)
                {
                    elemList[0].InnerText = state.ToString();
                    writeToLog("Changed Boolean: '" + varName + "' to a value of: '" + state + "'");
                    saveXML();
                    return true;
                }
            }
            return false;
        }

        public bool trySetBoolean(string varName, bool state)
        {
            if (checkIfXmlCompatible())
            {
                loadXML();
                XmlNodeList elemList = doc.GetElementsByTagName(varName);
                if (elemList.Count == 1)
                {
                    elemList[0].InnerText = state.ToString();
                    writeToLog("Changed Boolean: '" + varName + "' to a value of: '" + state + "'");
                    saveXML();
                    return true;
                }
                else
                {
                    return addBoolean(varName, state);
                }
            }
            return false;
        }
        /// <summary>
        /// Sets the String variable of an already existing variable in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <param name="state">The value that will be assigned to the variable.</param>
        /// <returns>If the variables was set successfully.</returns>
        public bool setString(string varName, string state)
        {
            if (checkIfXmlCompatible())
            {
                loadXML();
                XmlNodeList elemList = doc.GetElementsByTagName(varName);
                if (elemList.Count == 1)
                {
                    elemList[0].InnerText = state;
                    writeToLog("Changed String: '" + varName + "' to a value of: '" + state + "'");
                    saveXML();
                    return true;
                }
            }
            return false;
        }

        public bool trySetString(string varName, string state)
        {
            if (checkIfXmlCompatible())
            {
                loadXML();
                XmlNodeList elemList = doc.GetElementsByTagName(varName);
                if (elemList.Count == 1)
                {
                    elemList[0].InnerText = state;
                    writeToLog("Changed String: '" + varName + "' to a value of: '" + state + "'");
                    saveXML();
                    return true;
                }
                else
                {
                    return addString(varName, state);
                }
            }
            return false;
        }

        /// <summary>
        /// Sets the Int variable of an already existing variable in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <param name="state">The value that will be assigned to the variable.</param>
        /// <returns>If the variables was set successfully.</returns>
        public bool setInt(string varName, int state)
        {
            if (checkIfXmlCompatible())
            {
                loadXML();
                XmlNodeList elemList = doc.GetElementsByTagName(varName);
                if (elemList.Count == 1)
                {
                    elemList[0].InnerText = state.ToString();
                    writeToLog("Changed Int: '" + varName + "' to a value of: '" + state + "'");
                    saveXML();
                    return true;
                }
            }
            return false;
        }


        public bool trySetInt(string varName, int state)
        {
            if (checkIfXmlCompatible())
            {
                loadXML();
                XmlNodeList elemList = doc.GetElementsByTagName(varName);
                if (elemList.Count == 1)
                {
                    elemList[0].InnerText = state.ToString();
                    writeToLog("Changed Int: '" + varName + "' to a value of: '" + state + "'");
                    saveXML();
                    return true;
                }
                else
                {
                    return addInt(varName, state);
                }
            }
            return false;
        }

        /// <summary>
        /// Sets the Long variable of an already existing variable in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <param name="state">The value that will be assigned to the variable.</param>
        /// <returns>If the variables was set successfully.</returns>
        public bool setLong(string varName, long state)
        {
            if (checkIfXmlCompatible())
            {
                loadXML();
                XmlNodeList elemList = doc.GetElementsByTagName(varName);
                if (elemList.Count == 1)
                {
                    elemList[0].InnerText = state.ToString();
                    writeToLog("Changed Long: '" + varName + "' to a value of: '" + state + "'");
                    saveXML();
                    return true;
                }
            }
            return false;
        }

        public bool trySetLong(string varName, long state)
        {
            if (checkIfXmlCompatible())
            {
                loadXML();
                XmlNodeList elemList = doc.GetElementsByTagName(varName);
                if (elemList.Count == 1)
                {
                    elemList[0].InnerText = state.ToString();
                    writeToLog("Changed Long: '" + varName + "' to a value of: '" + state + "'");
                    saveXML();
                    return true;
                }
                else
                {
                    return addLong(varName, state);
                }
            }
            return false;
        }

        /// <summary>
        /// Sets the Double variable of an already existing variable in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <param name="state">The value that will be assigned to the variable.</param>
        /// <returns>If the variables was set successfully.</returns>
        public bool setDouble(string varName, double state)
        {
            if (checkIfXmlCompatible())
            {
                loadXML();
                XmlNodeList elemList = doc.GetElementsByTagName(varName);
                if (elemList.Count == 1)
                {
                    elemList[0].InnerText = state.ToString();
                    writeToLog("Changed Double: '" + varName + "' to a value of: '" + state + "'");
                    saveXML();
                    return true;
                }
            }
            return false;
        }

        public bool trySetDouble(string varName, double state)
        {
            if (checkIfXmlCompatible())
            {
                loadXML();
                XmlNodeList elemList = doc.GetElementsByTagName(varName);
                if (elemList.Count == 1)
                {
                    elemList[0].InnerText = state.ToString();
                    writeToLog("Changed Double: '" + varName + "' to a value of: '" + state + "'");
                    saveXML();
                    return true;
                }
                else
                {
                    return addDouble(varName, state);
                }
            }
            return false;
        }
        /// <summary>
        /// Reads the Boolean variable of an already existing variable in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the value was read successfully.</returns>
        public bool readBoolean(string varName)
        {
            if (checkIfXmlCompatible())
            {
                try
                {
                    loadXML();
                    XmlNodeList elemList = doc.GetElementsByTagName(varName);
                    writeToLog("Reading Boolean: '" + varName + "'");
                    if (elemList.Count > 0)
                    {
                        return bool.Parse(elemList[0].InnerXml);
                    }
                }
                catch
                {
                    if (revertOnFail)
                    {
                        revertToDefault(varName);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Reads the String variable of an already existing variable in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the value was read successfully.</returns>
        public string readString(string varName)
        {
            if (checkIfXmlCompatible())
            {
                try
                {
                    loadXML();
                    XmlNodeList elemList = doc.GetElementsByTagName(varName);
                    writeToLog("Reading String: '" + varName + "'");
                    if (elemList.Count > 0)
                    {
                        return elemList[0].InnerXml.ToString();
                    }
                }
                catch
                {
                    if (revertOnFail)
                    {
                        revertToDefault(varName);
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// Reads the Int variable of an already existing variable in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the value was read successfully.</returns>
        public int readInt(string varName)
        {
            if (checkIfXmlCompatible())
            {
                try
                {
                    loadXML();
                    XmlNodeList elemList = doc.GetElementsByTagName(varName);
                    writeToLog("Reading Int: '" + varName + "'");
                    if (elemList.Count > 0)
                    {
                        return int.Parse(elemList[0].InnerXml);
                    }
                }
                catch
                {
                    if (revertOnFail)
                    {
                        revertToDefault(varName);
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Reads the Long variable of an already existing variable in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the value was read successfully.</returns>
        public long readLong(string varName)
        {
            if (checkIfXmlCompatible())
            {
                try
                {
                    loadXML();
                    XmlNodeList elemList = doc.GetElementsByTagName(varName);
                    writeToLog("Reading Long: '" + varName + "'");
                    if (elemList.Count > 0)
                    {
                        return long.Parse(elemList[0].InnerXml);
                    }
                }
                catch
                {
                    if (revertOnFail)
                    {
                        revertToDefault(varName);
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Reads the Double variable of an already existing variable in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the value was read successfully.</returns>
        public double readDouble(string varName)
        {
            if (checkIfXmlCompatible())
            {
                try
                {
                    loadXML();
                    XmlNodeList elemList = doc.GetElementsByTagName(varName);
                    writeToLog("Reading Double: '" + varName + "'");
                    if (elemList.Count > 0)
                    {
                        return double.Parse(elemList[0].InnerXml);
                    }
                }
                catch
                {
                    if (revertOnFail)
                    {
                        revertToDefault(varName);
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Removes the specified Boolean variable from the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the variable was removed sucessfully.</returns>
        public bool removeBoolean(string varName)
        {
            writeToLog("Attempting to remove Boolean: '" + varName + "'");
            if (getVarType(varName) == "Boolean")
            {
                return removeXmlElement(varName);
            }

            return false;
        }

        /// <summary>
        /// Removes the specified String variable from the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the variable was removed sucessfully.</returns>
        public bool removeString(string varName)
        {
            writeToLog("Attempting to remove String: '" + varName + "'");
            if (getVarType(varName) == "String")
            {
                return removeXmlElement(varName);
            }

            return false;
        }

        /// <summary>
        /// Removes the specified Int variable from the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the variable was removed sucessfully.</returns>
        public bool removeInt(string varName)
        {
            writeToLog("Attempting to remove Int: '" + varName + "'");
            if (getVarType(varName) == "Int")
            {
                return removeXmlElement(varName);
            }

            return false;
        }

        /// <summary>
        /// Removes the specified Long variable from the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the variable was removed sucessfully.</returns>
        public bool removeLong(string varName)
        {
            writeToLog("Attempting to remove Long: '" + varName + "'");
            if (getVarType(varName) == "Long")
            {
                return removeXmlElement(varName);
            }

            return false;
        }

        /// <summary>
        /// Removes the specified Double variable from the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the variable was removed sucessfully.</returns>
        public bool removeDouble(string varName)
        {
            writeToLog("Attempting to remove Double: '" + varName + "'");
            if (getVarType(varName) == "Double")
            {
                return removeXmlElement(varName);
            }

            return false;
        }

        /// <summary>
        /// Removes the XML Element of the specified varName, not accessible publicly to avoid removing incorrect variables.
        /// </summary>
        /// <param name="varName">The name of the variable to remove.</param>
        /// <returns>If the XML Element was removed sucessfully.</returns>
        private bool removeXmlElement(string varName)
        {
            if (checkIfXmlCompatible())
            {
                try
                {
                    loadXML();
                    XmlNodeList elemList = doc.GetElementsByTagName(varName);
                    if (elemList.Count > 0)
                    {
                        elemList[0].ParentNode.RemoveAll();
                        saveXML();
                        deleteEmptyParents();
                        return true;
                    }
                }
                catch
                {
                    if (revertOnFail)
                    {
                        revertToDefault(varName);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Reverts the specified variable to its default setting.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the variable was reverted successfully.</returns>
        public bool revertToDefault(string varName)
        {
            loadXML();
            XmlNodeList elemList = doc.GetElementsByTagName(varName);
            writeToLog("Reverting: '" + varName + "' to its default value");
            if (elemList.Count == 1)
            {
                elemList[0].InnerText = elemList[0].Attributes["default"].Value;
                saveXML();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns if a specified variable exists in the XML File.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>If the variable exists.</returns>
        public bool doesVariableExist(string varName)
        {
            loadXML();
            XmlNodeList elemList = doc.GetElementsByTagName(varName);
            if (elemList.Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the Variable type of the specified variable.
        /// </summary>
        /// <param name="varName">The name of the variable in the XML File.</param>
        /// <returns>The name of the variable type.</returns>
        public string getVarType(string varName)
        {
            loadXML();
            XmlNodeList elemList = doc.GetElementsByTagName(varName);
            if (elemList.Count > 0)
            {
                return uppercaseFirst(elemList[0].ParentNode.Name);
            }

            return null;
        }

        /// <summary>
        /// Cleans up the XML file. Deleting any unneeded information or moving nodes around.
        /// </summary>
        public void cleanupXML()
        {
            condenseXMLNodes();
            setXmlSettingsEncodingVersion(getEncodingVersion());
            deleteEmptyParents();
            loadXML();
        }

        /// <summary>
        /// Deletes any leftover, empty, parent nodes from removing variables.
        /// </summary>
        private void deleteEmptyParents()
        {
            loadXML();

            for (int i = 0; i < doc.ChildNodes.Count; i++)
            {
                for (int n = 0; n < doc.ChildNodes[i].ChildNodes.Count; n++)
                {
                    XmlNode node = doc.ChildNodes[i].ChildNodes[n];
                    if (node.ChildNodes.Count == 0)
                    {
                        writeToLog("Purging empty parent node: '" + node.Name + "'");
                        node.ParentNode.RemoveChild(node);
                    }
                }
            }
            saveXML();
        }

        /// <summary>
        /// Condenses XML Nodes of the same type into one parent (Updates 1.0 formatting to 1.1).
        /// </summary>
        private void condenseXMLNodes()
        {
            deleteEmptyParents();

            XmlNodeList intNodes = doc.SelectNodes("//int");
            XmlNodeList longNodes = doc.SelectNodes("//long");
            XmlNodeList boolNodes = doc.SelectNodes("//boolean");
            XmlNodeList stringNodes = doc.SelectNodes("//string");
            XmlNodeList doubleNodes = doc.SelectNodes("//double");

            if (intNodes.Count > 1 || longNodes.Count > 1 || boolNodes.Count > 1 || stringNodes.Count > 1 || doubleNodes.Count > 1)
            {
                writeToLog("Uncondensed XML nodes detected: Condensing XML nodes", false, true);

                performXMLBackup();

                List<XmlNode> childIntNodes = new List<XmlNode>();
                List<XmlNode> childLongNodes = new List<XmlNode>();
                List<XmlNode> childBoolNodes = new List<XmlNode>();
                List<XmlNode> childStringNodes = new List<XmlNode>();
                List<XmlNode> childDoubleNodes = new List<XmlNode>();

                foreach (XmlNode node in intNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        childIntNodes.Add(childNode);
                    }
                }

                foreach (XmlNode node in longNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        childLongNodes.Add(childNode);
                    }
                }

                foreach (XmlNode node in boolNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        childBoolNodes.Add(childNode);
                    }
                }

                foreach (XmlNode node in stringNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        childStringNodes.Add(childNode);
                    }
                }

                foreach (XmlNode node in doubleNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        childDoubleNodes.Add(childNode);
                    }
                }

                deleteXMLFile(false);
                createXMLFile();

                XmlNode intNode = addToHeadingNode("int");
                XmlNode longNode = addToHeadingNode("long");
                XmlNode boolNode = addToHeadingNode("boolean");
                XmlNode stringNode = addToHeadingNode("string");
                XmlNode doubleNode = addToHeadingNode("double");

                xmlBody.AppendChild(intNode);
                xmlBody.AppendChild(longNode);
                xmlBody.AppendChild(boolNode);
                xmlBody.AppendChild(stringNode);
                xmlBody.AppendChild(doubleNode);

                try
                {
                    foreach (XmlNode node in childIntNodes)
                    {
                        intNode.AppendChild(doc.ImportNode(node, true));
                    }

                    foreach (XmlNode node in childLongNodes)
                    {
                        longNode.AppendChild(doc.ImportNode(node, true));
                    }

                    foreach (XmlNode node in childBoolNodes)
                    {
                        boolNode.AppendChild(doc.ImportNode(node, true));
                    }

                    foreach (XmlNode node in childStringNodes)
                    {
                        stringNode.AppendChild(doc.ImportNode(node, true));
                    }

                    foreach (XmlNode node in childDoubleNodes)
                    {
                        doubleNode.AppendChild(doc.ImportNode(node, true));
                    }

                    saveXML();
                    reloadXMLSettings();
                }
                catch (Exception e)
                {
                    writeToLog("An error occurred while trying to condense the XML nodes! Error: " + e.ToString(), true);
                }
            }
        }

        /// <summary>
        /// Saves the XML file loaded in memory to the disk.
        /// </summary>
        private void saveXML()
        {
            doc.Save(mainSettingsPath);
        }

        /// <summary>
        /// Loads the XML file from disk into memory.
        /// </summary>
        /// <param name="skipRootCatch">Toggles whether the file should be recreated if the root node is missing.</param>
        private void loadXML(bool skipRootCatch = false)
        {
            if (checkIfXmlCompatible())
            {
                try
                {
                    doc.Load(mainSettingsPath);
                    xmlContents = doc.InnerXml;
                    xmlBody = doc.DocumentElement;
                }
                catch (Exception e)
                {
                    if (purgeOnRootFailure && !skipRootCatch)
                    {
                        writeToLog("The XML root node is missing. Deleting and recreating the file...", false, true);
                        recreateXMLFile(false);
                    }
                    else
                    {
                        writeToLog("The XML root node is missing. Not deleting the file on request. Error: " + e.ToString(), true);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new XML file with the valid Declarations and Elements.
        /// </summary>
        /// 
        public void createXMLFile()
        {
            if (lockXML)
            {
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;

                XmlElement pi = doc.CreateElement("XmlSettingsEncodingVersion");
                pi.InnerText = getEncodingVersion();

                doc.InsertBefore(xmlDeclaration, root);

                xmlBody = doc.CreateElement(string.Empty, "body", string.Empty);
                doc.AppendChild(xmlBody);
                xmlBody.AppendChild(pi);

                lockXML = false;

                writeToLog("A new XMLSettings file has been created!");
                saveXML();
            }
            else
            {
                writeToLog("Cannot create a new XMLSettings file since another is already loaded and in use.", false, true);
            }
        }

        /// <summary>
        /// Recreates an empty XML file with the valid Declarations and Elements.
        /// </summary>
        private void recreateXMLFile(bool deleteBackups = true, bool skipBackup = false)
        {
            if (performBackups && !deleteBackups && !skipBackup)
            {
                performXMLBackup();
            }

            deleteXMLFile(deleteBackups);
            createXMLFile();
            loadXML();
        }

        /// <summary>
        /// Creates a backup of the XML file.
        /// </summary>
        public void performXMLBackup()
        {
            if (File.Exists(mainSettingsPath))
            {
                if (File.Exists(backupPath))
                {
                    File.Delete(backupPath);
                }

                File.Copy(mainSettingsPath, backupPath);
            }
        }

        /// <summary>
        /// Deletes the XML file backups.
        /// </summary>
        public void deleteXMLBackups()
        {
            writeToLog("XMLSettings backup has been deleted.", false, true);
            File.Delete(backupPath);
        }

        /// <summary>
        /// Deletes the entire XML File and locks out any modification or read attempts.
        /// </summary>
        /// <param name="deleteBackups">Also deletes the XML file backups.</param>
        public void deleteXMLFile(bool deleteBackups = true)
        {
            lockXMLSettings();

            if (deleteBackups)
            {
                deleteXMLBackups();
            }

            File.Delete(mainSettingsPath);
        }

        /// <summary>
        /// Checks if another variable exists with the same name.
        /// </summary>
        /// <param name="varName">Variable name to check.</param>
        /// <returns>A boolean depending on if the variable already exists.</returns>
        private bool doesDuplicateExist(string varName)
        {
            loadXML();
            XmlNodeList elemList = doc.GetElementsByTagName(varName);
            if (elemList.Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Capitalises the first letter of a string.
        /// </summary>
        /// <param name="s">String use as source.</param>
        /// <returns>The original string with the first letter capitalised.</returns>
        private string uppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// Writes a string to the log file.
        /// </summary>
        /// <param name="log">Message to add to log file.</param>
        /// <param name="isError">Whether the log message is an error.</param>
        private void writeToLog(string log, bool isError = false, bool isWarn = false)
        {
            if (!String.IsNullOrEmpty(log))
            {
                if (isError)
                {
                    File.AppendAllText(errorLogPathLoc, "[" + DateTime.Now.ToString("hh:mm:ss tt") + "] [ERROR] " + log + Environment.NewLine);
                }
                else if (verboseLogging && isWarn)
                {
                    File.AppendAllText(errorLogPathLoc, "[" + DateTime.Now.ToString("hh:mm:ss tt") + "] [WARN] " + log + Environment.NewLine);
                }
                else if (verboseLogging)
                {
                    File.AppendAllText(errorLogPathLoc, "[" + DateTime.Now.ToString("hh:mm:ss tt") + "] [LOG] " + log + Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// Checks to see if the specified node exists and selects it, if not it will create a new one (and selects that).
        /// </summary>
        /// <param name="headingName">Node to check for.</param>
        /// <returns>An XMLNode of the specified node.</returns>
        private XmlNode addToHeadingNode(string headingName)
        {
            XmlNode node;

            if (doc.DocumentElement[headingName] != null)
            {
                node = doc.DocumentElement[headingName];
            }
            else
            {
                node = doc.CreateElement(headingName);
                xmlBody.AppendChild(node);
            }
            return node;
        }

        /// <summary>
        /// Checks to ensure the specified XML file is compatible with the current version of XMLSettings.
        /// </summary>
        /// <returns>A boolean of if the XML file is supported.</returns>
        public bool checkIfXmlCompatible()
        {
            if (lockXML)
            {
                writeToLog("This XMLSettings file has been locked and cannot be read from or written to.", true);
            }

            return isXmlCompatible;
        }

        /// <summary>
        /// Does various checks to the XML file on initialization.
        /// </summary>
        private void checkXML()
        {
            if (!isXmlFileVerSupported())
            {
                doc = null;
                writeToLog("The specified XMLSettings file has not been encoded in a supported XmlSettings version!", true);

                if (purgeOnUnsupportedVer)
                {
                    writeToLog("Deleting unsupported XMLSettings File.", false, true);
                    recreateXMLFile(false);
                }
                else
                {
                    isXmlCompatible = false;
                }
            }
        }

        /// <summary>
        /// Parses the version into an int array.
        /// </summary>
        /// <param name="version">Version as a string.</param>
        /// <returns>Version as an int array.</returns>
        private int[] parseVersion(string version)
        {
            if (!String.IsNullOrEmpty(version))
            {
                string[] sVersionAsArray = version.Split('.');
                List<int> iVersionAsList = new List<int>();

                foreach (String s in sVersionAsArray)
                {
                    iVersionAsList.Add(Convert.ToInt32(s));
                }

                return iVersionAsList.ToArray();
            }
            return null;
        }

        /// <summary>
        /// Gets if the XML File Version is supported.
        /// </summary>
        /// <returns>If the XML file is supported.</returns>
        private bool isXmlFileVerSupported()
        {
            loadXmlDeclaration();
            loadXMLInformation();

            int[] fileVersion = parseVersion(getFileXmlSettingsVersion());
            int[] libVersion = parseVersion(getEncodingVersion());
            int[] supVersion = parseVersion(getMinSupportedEncodingVersion());

            int longestVersionArray = fileVersion.Length;
            if (libVersion.Length > longestVersionArray)
            {
                longestVersionArray = libVersion.Length;
            }

            if (fileVersion[0] > supVersion[0])
            {
                if (fileVersion[0] > libVersion[0])
                {
                    return false;
                }
            }

            if (fileVersion[1] > supVersion[1])
            {
                if (fileVersion[1] > libVersion[1])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Loads the XML Declatation.
        /// </summary>
        private void loadXmlDeclaration()
        {
            loadXML();
            try
            {
                if (doc.ChildNodes[0].NodeType == XmlNodeType.XmlDeclaration)
                {
                    xmlDeclaration = doc.ChildNodes[0] as XmlDeclaration;
                    xmlFileFormatVer = xmlDeclaration.Version;
                    xmlFileFormat = xmlDeclaration.Encoding;
                }
                else if (!purgeIfXmlDecMissing)
                {
                    writeToLog("The XML Declaration is missing. Assuming the version as '1.0' and encoding as 'UTF-8'.", false, true);
                    xmlFileFormatVer = "1.0";
                    xmlFileFormat = "UTF-8";
                }
                else
                {
                    writeToLog("The XML Declaration is missing. Deleting and recreating the file...", false, true);
                    recreateXMLFile(false);
                }
            }
            catch (Exception e)
            {
                writeToLog("An error occurred while trying to load the XML declaration! Error: " + e.ToString(), true);
            }
        }

        /// <summary>
        /// Sets the XML Encoding Version variable in the XML file.
        /// </summary>
        /// <param name="version">The new version.</param>
        private void setXmlSettingsEncodingVersion(string version)
        {
            loadXML();
            XmlNodeList elemList = doc.GetElementsByTagName("XmlSettingsEncodingVersion");
            if (elemList.Count > 0)
            {
                elemList[0].InnerText = version;
            }
            else
            {
                XmlElement pi = doc.CreateElement("XmlSettingsEncodingVersion");
                pi.InnerText = version;

                doc.DocumentElement.InsertBefore(pi, doc.DocumentElement.FirstChild);
            }
            saveXML();
        }

        /// <summary>
        /// Loads the XmlSettings Metadata from the XML file.
        /// </summary>
        private void loadXMLInformation()
        {
            loadXML();
            try
            {
                XmlNodeList elemList = doc.GetElementsByTagName("XmlSettingsEncodingVersion");
                if (elemList.Count > 0)
                {
                    xmlSettingsFileVer = elemList[0].InnerXml;
                }
                else
                {
                    writeToLog("The XMLSettings Metadata information is missing. Assuming the encoded XMLSettings version was '1.0'.", false, true);
                    xmlSettingsFileVer = "1.0";
                }
            }
            catch (Exception e)
            {
                writeToLog(e.ToString(), true);
            }
        }

        /// <summary>
        /// Locks the XML file to disable read and writes.
        /// </summary>
        private void lockXMLSettings()
        {
            doc = new XmlDocument();
            xmlBody = null;
            xmlContents = xmlFileFormatVer = xmlFileFormat = xmlSettingsFileVer = null;
            xmlDeclaration = null;
            isXmlCompatible = false;
            lockXML = true;
        }
    }
}