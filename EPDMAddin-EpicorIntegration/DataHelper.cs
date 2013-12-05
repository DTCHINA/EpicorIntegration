using System;
using System.Collections.Generic;
using System.Text;
using EdmLib;

namespace Epicor_Integration
{
    public class DataHelper
    {
        /// <summary>
        /// Gets a list of structs based on the EdmCmdType.
        /// </summary>
        /// <typeparam name="TType">The type to return.</typeparam>
        /// <param name="data">An array with one struct per affected file or folder.</param>
        /// <param name="edmCmd">Command information common to all affected files and folders.</param>
        /// <returns>A list of EdmCmdType</returns>
        public List<TType> GetInfo<TType>(Array data, EdmCmd edmCmd) where TType : struct
        {
            List<TType> result = new List<TType>();
            switch (edmCmd.meCmdType)
            {
                #region EdmCmdCardButton
                case EdmCmdType.EdmCmd_CardButton:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdCardButton(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mbsStrData1,
                            item.mbsStrData2,
                            item.mlLongData1,
                            item.mlLongData2,
                            edmCmd.mpoExtra,
                            edmCmd.mbsComment);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdCardInput
                case EdmCmdType.EdmCmd_CardInput:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdCardInput(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mlObjectID4,
                            item.mbsStrData1,
                            item.mbsStrData2,
                            item.mlLongData1,
                            edmCmd.mpoExtra,
                            edmCmd.mbsComment);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdCardListSrc
                case EdmCmdType.EdmCmd_CardListSrc:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdCardListSrc(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mlObjectID4,
                            item.mbsStrData1,
                            item.mbsStrData2,
                            item.mbsStrData3,
                            item.mlLongData1,
                            edmCmd.mpoExtra,
                            edmCmd.mbsComment);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                case EdmCmdType.EdmCmd_InstallAddIn:
                    break;
                #region EdmCmdMenu
                case EdmCmdType.EdmCmd_Menu:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdMenu(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPostAdd
                case EdmCmdType.EdmCmd_PostAdd:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPostAdd(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mbsStrData1,
                            item.mlLongData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPostAddFolder
                case EdmCmdType.EdmCmd_PostAddFolder:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPostAddFolder(
                            item.mlObjectID1,
                            item.mlObjectID3,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPostCopyFolder
                case EdmCmdType.EdmCmd_PostCopyFolder:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPostCopyFolder(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPostDelete
                case EdmCmdType.EdmCmd_PostDelete:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPostDelete(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mbsStrData1,
                            item.mlLongData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostLock
                case EdmCmdType.EdmCmd_PreLock:
                case EdmCmdType.EdmCmd_PostLock:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostLock(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostMove
                case EdmCmdType.EdmCmd_PreMove:
                case EdmCmdType.EdmCmd_PostMove:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostMove(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mbsStrData1,
                            item.mbsStrData2);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostMoveFolder
                case EdmCmdType.EdmCmd_PreMoveFolder:
                case EdmCmdType.EdmCmd_PostMoveFolder:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostMoveFolder(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mbsStrData1,
                            item.mbsStrData2);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostRename
                case EdmCmdType.EdmCmd_PreRename:
                case EdmCmdType.EdmCmd_PostRename:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostRename(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mbsStrData1,
                            item.mbsStrData2);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostRenameFolder
                case EdmCmdType.EdmCmd_PreRenameFolder:
                case EdmCmdType.EdmCmd_PostRenameFolder:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostRenameFolder(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mbsStrData1,
                            item.mbsStrData2);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostShare
                case EdmCmdType.EdmCmd_PreShare:
                case EdmCmdType.EdmCmd_PostShare:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostShare(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostState
                case EdmCmdType.EdmCmd_PreState:
                case EdmCmdType.EdmCmd_PostState:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostState(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mlObjectID4,
                            item.mbsStrData1,
                            item.mbsStrData2,
                            item.mlLongData1,
                            item.mlLongData2);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostUndoLock
                case EdmCmdType.EdmCmd_PreUndoLock:
                case EdmCmdType.EdmCmd_PostUndoLock:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostUndoLock(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostUnlock
                case EdmCmdType.EdmCmd_PreUnlock:
                case EdmCmdType.EdmCmd_PostUnlock:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostUnlock(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAdd
                case EdmCmdType.EdmCmd_PreAdd:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAdd(
                            item.mlObjectID1,
                            item.mbsStrData1,
                            item.mlLongData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAddFolder
                case EdmCmdType.EdmCmd_PreAddFolder:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAddFolder(
                            item.mlObjectID3,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostCopy
                case EdmCmdType.EdmCmd_PreCopy:
                case EdmCmdType.EdmCmd_PostCopy:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostCopy(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mbsStrData1,
                            item.mbsStrData2);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreCopyFolder
                case EdmCmdType.EdmCmd_PreCopyFolder:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreCopyFolder(
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreDelete
                case EdmCmdType.EdmCmd_PreDelete:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreDelete(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostDeleteFolder
                case EdmCmdType.EdmCmd_PreDeleteFolder:
                case EdmCmdType.EdmCmd_PostDeleteFolder:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostDeleteFolder(
                            item.mlObjectID1,
                            item.mbsStrData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdPreAndPostGet
                case EdmCmdType.EdmCmd_PreGet:
                case EdmCmdType.EdmCmd_PostGet:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdPreAndPostGet(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mbsStrData1,
                            item.mlLongData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                #region EdmCmdSerialNo
                case EdmCmdType.EdmCmd_SerialNo:
                    foreach (EdmCmdData item in data)
                    {
                        object obj = new EdmCmdSerialNo(
                            item.mlObjectID1,
                            item.mlObjectID2,
                            item.mlObjectID3,
                            item.mlObjectID4,
                            item.mbsStrData1,
                            item.mbsStrData2,
                            item.mbsStrData3,
                            item.mlLongData1);
                        result.Add((TType)obj);
                    }
                    break;
                #endregion
                case EdmCmdType.EdmCmd_UninstallAddIn:
                    break;
                default:
                    break;
            }
            return result;
        }
    }
    #region structs
    /// <summary>
    /// The user has activated a menu item (or toolbar button) that your add-in has added.
    /// </summary>
    public struct EdmCmdMenu
    {
        /// <summary>
        /// ID of file (or zero if a folder is selected).
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of folder (or zero if a file is selected).
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// ID of parent folder of the selected file or folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// Name of file or folder. (Only the name, not a full path.)
        /// </summary>
        public string FileOrFolderName { get; set; }
        public EdmCmdMenu(int mlObjectID1, int mlObjectID2, int mlObjectID3, string mbsStrData1)
            : this()
        {
            FileID = mlObjectID1;
            FolderID = mlObjectID2;
            ParentFolderID = mlObjectID3;
            FileOrFolderName = mbsStrData1;
        }
    }
    /// <summary>
    /// The user has pressed a button in a file/folder data card and the button is connected to an add-in.
    /// This notification is sent when the user presses the OK or Apply button in the card too.
    /// </summary>
    public struct EdmCmdCardButton
    {
        /// <summary>
        /// ID of file for which the card is displayed. (Zero for folder cards.)
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of folder. (Parent folder ID for file data cards.)
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// ID of file data card.
        /// </summary>
        public int DataCardID { get; set; }
        /// <summary>
        /// Name of active configuration. (Can be changed to switch to a new configuration.)
        /// </summary>
        public string ConfigurationName { get; set; }
        /// <summary>
        /// Path to file.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Optionally return a EdmCardFlag return code here.
        /// </summary>
        public EdmCardFlag CardFlag { get; set; }
        /// <summary>
        /// Optionally return the ID of a card control to set focus to here.
        /// </summary>
        public int CardControlID { get; set; }
        /// <summary>
        /// Ptr to an IEdmStrLst5 interface with the names of all configurations.
        /// </summary>
        public IEdmStrLst5 ConfigurationNames { get; set; }
        public IEdmEnumeratorVariable5 EnumeratorVariable { get; set; }
        public IEdmCard5 Card { get; set; }
        public string ButtonCommand { get; set; }
        public EdmCmdCardButton(int mlObjectID1, int mlObjectID2, int mlObjectID3, string mbsStrData1,
            string mbsStrData2, int mlLongData1, int mlLongData2, object mpoExtra, string mbsComment)
            : this()
        {
            FileID = mlObjectID1;
            FolderID = mlObjectID2;
            DataCardID = mlObjectID3;
            ConfigurationName = mbsStrData1;
            FilePath = mbsStrData2;
            CardFlag = (EdmCardFlag)mlLongData1;
            CardControlID = mlLongData2;
            ConfigurationNames = (IEdmStrLst5)mpoExtra;
            EnumeratorVariable = (IEdmEnumeratorVariable5)mpoExtra;
            Card = (IEdmCard5)mpoExtra;
            ButtonCommand = mbsComment;
        }
    }
    /// <summary>
    /// The user has modified some data in a file/folder data card. (For instance by typing in a letter in an editbox.)
    /// </summary>
    public struct EdmCmdCardInput
    {
        /// <summary>
        /// ID of the modified card control.
        /// </summary>
        public int CardControlID { get; set; }
        /// <summary>
        /// ID of the file (zero for folder cards).
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of folder.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// ID of data card.
        /// </summary>
        public int DataCardID { get; set; }
        /// <summary>
        /// Name of active configuration.
        /// </summary>
        public string ConfigurationName { get; set; }
        /// <summary>
        /// Full path to the file.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// ID of the updated variable.
        /// </summary>
        public int VariableID { get; set; }
        /// <summary>
        /// Ptr to an IEdmStrLst5 interface with the names of all configurations.
        /// </summary>
        public IEdmStrLst5 ConfigurationNames { get; set; }
        public IEdmEnumeratorVariable5 EnumeratorVariable { get; set; }
        public IEdmCard5 Card { get; set; }
        public string VariableName { get; set; }
        public EdmCmdCardInput(int mlObjectID1, int mlObjectID2, int mlObjectID3, int mlObjectID4, string mbsStrData1,
            string mbsStrData2, int mlLongData1, object mpoExtra, string mbsComment)
            : this()
        {
            CardControlID = mlObjectID1;
            FileID = mlObjectID2;
            FolderID = mlObjectID3;
            DataCardID = mlObjectID4;
            ConfigurationName = mbsStrData1;
            FilePath = mbsStrData2;
            VariableID = mlLongData1;
            ConfigurationNames = (IEdmStrLst5)mpoExtra;
            EnumeratorVariable = (IEdmEnumeratorVariable5)mpoExtra;
            Card = (IEdmCard5)mpoExtra;
            VariableName = mbsComment;
        }
    }
    /// <summary>
    /// A file or folder data card containing a list box or combo box is displayed. 
    /// The add-in’s OnCmd method is given the opportunity to fill-in the rows in the list instead of using the list contents defined in the card editor.
    /// </summary>
    public struct EdmCmdCardListSrc
    {
        /// <summary>
        /// ID of the card control.
        /// </summary>
        public int CardControlID { get; set; }
        /// <summary>
        /// ID of the file (zero for folder cards).
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of folder.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// ID of data card.
        /// </summary>
        public int DataCardID { get; set; }
        /// <summary>
        /// Name of active configuration.
        /// </summary>
        public string ConfigurationName { get; set; }
        /// <summary>
        /// Full path to the file.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// The name of the control’s variable.
        /// </summary>
        public string VariableName { get; set; }
        /// <summary>
        /// ID of the control's variable.
        /// </summary>
        public int VariableID { get; set; }
        /// <summary>
        /// Ptr to an IEdmStrLst5 interface with the names of all configurations.
        /// </summary>
        public IEdmStrLst5 ConfigurationNames { get; set; }
        public IEdmEnumeratorVariable5 EnumeratorVariable { get; set; }
        public IEdmCard5 Card { get; set; }
        /// <summary>
        /// The contents of the EdmCmd::mbsComment member is the return value from your OnCmd implementation.
        /// This variable should be set to a newline-delimited list of strings to be inserted into the list box or combo box.
        /// Leave this variable untouched to use the standard values from the card editor.
        /// </summary>
        public string OnCmdValue { get; set; }
        public EdmCmdCardListSrc(int mlObjectID1, int mlObjectID2, int mlObjectID3, int mlObjectID4, string mbsStrData1,
            string mbsStrData2, string mbsStrData3, int mlLongData1, object mpoExtra, string mbsComment)
            : this()
        {
            CardControlID = mlObjectID1;
            FileID = mlObjectID2;
            FolderID = mlObjectID3;
            DataCardID = mlObjectID4;
            ConfigurationName = mbsStrData1;
            FilePath = mbsStrData2;
            VariableName = mbsStrData3;
            VariableID = mlLongData1;
            ConfigurationNames = (IEdmStrLst5)mpoExtra;
            EnumeratorVariable = (IEdmEnumeratorVariable5)mpoExtra;
            Card = (IEdmCard5)mpoExtra;
            OnCmdValue = mbsComment;
        }
    }
    /// <summary>
    /// A file is about to be added.
    /// </summary>
    public struct EdmCmdPreAdd
    {
        /// <summary>
        /// ID of parent folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// Local file path.
        /// </summary>
        public string LocalFilePath { get; set; }
        /// <summary>
        /// 0 for normal files, 1 for network sharing links.
        /// </summary>
        public int NormalOrShared { get; set; }

        public EdmCmdPreAdd(int mlObjectID1, string mbsStrData1, int mlLongData1)
            : this()
        {
            ParentFolderID = mlObjectID1;
            LocalFilePath = mbsStrData1;
            NormalOrShared = mlLongData1;
        }
    }
    /// <summary>
    /// A file has been added.
    /// </summary>
    public struct EdmCmdPostAdd
    {
        /// <summary>
        /// ID of parent folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// ID of file.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// Local file path.
        /// </summary>
        public string LocalFilePath { get; set; }
        /// <summary>
        /// 0 for normal files, 1 for network sharing links.
        /// </summary>
        public int NormalOrShared { get; set; }

        public EdmCmdPostAdd(int mlObjectID1, int mlObjectID2, string mbsStrData1, int mlLongData1)
            : this()
        {
            ParentFolderID = mlObjectID1;
            FileID = mlObjectID2;
            LocalFilePath = mbsStrData1;
            NormalOrShared = mlLongData1;
        }
    }
    /// <summary>
    /// A folder is about to be added.
    /// </summary>
    public struct EdmCmdPreAddFolder
    {
        /// <summary>
        /// ID of parent folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// Path to new folder.
        /// </summary>
        public string NewFolderPath { get; set; }

        public EdmCmdPreAddFolder(int mlObjectID3, string mbsStrData1)
            : this()
        {
            ParentFolderID = mlObjectID3;
            NewFolderPath = mbsStrData1;
        }
    }
    /// <summary>
    /// A folder has been added.
    /// </summary>
    public struct EdmCmdPostAddFolder
    {
        /// <summary>
        /// ID of new folder.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// ID of parent folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// Path to new folder.
        /// </summary>
        public string NewFolderPath { get; set; }

        public EdmCmdPostAddFolder(int mlObjectID1, int mlObjectID3, string mbsStrData1)
            : this()
        {
            FolderID = mlObjectID1;
            ParentFolderID = mlObjectID3;
            NewFolderPath = mbsStrData1;
        }
    }
    /// <summary>
    /// A file is copied.
    /// </summary>
    public struct EdmCmdPreAndPostCopy
    {
        /// <summary>
        /// ID of destination folder.
        /// </summary>
        public int DestinationFolderID { get; set; }
        /// <summary>
        /// ID of file.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of source folder.
        /// </summary>
        public int SourceFolderID { get; set; }
        /// <summary>
        /// Source file path.
        /// </summary>
        public string SourceFilePath { get; set; }
        /// <summary>
        /// Destination file path.
        /// </summary>
        public string DestinationFilePath { get; set; }
        public EdmCmdPreAndPostCopy(int mlObjectID1, int mlObjectID2, int mlObjectID3, string mbsStrData1, string mbsStrData2)
            : this()
        {
            DestinationFolderID = mlObjectID1;
            FileID = mlObjectID2;
            SourceFolderID = mlObjectID3;
            SourceFilePath = mbsStrData1;
            DestinationFilePath = mbsStrData2;
        }
    }
    /// <summary>
    /// A folder is about to be copied.
    /// </summary>
    public struct EdmCmdPreCopyFolder
    {
        /// <summary>
        /// Destination parent folder ID.
        /// </summary>
        public int DestinationParentFolderID { get; set; }
        /// <summary>
        /// ID of source folder.
        /// </summary>
        public int SourceFolderID { get; set; }
        /// <summary>
        /// Path of new folder.
        /// </summary>
        public string NewFolderPath { get; set; }
        public EdmCmdPreCopyFolder(int mlObjectID2, int mlObjectID3, string mbsStrData1)
            : this()
        {
            DestinationParentFolderID = mlObjectID3;
            SourceFolderID = mlObjectID2;
            NewFolderPath = mbsStrData1;
        }
    }
    /// <summary>
    /// A folder is about to be copied.
    /// </summary>
    public struct EdmCmdPostCopyFolder
    {
        /// <summary>
        /// ID of new folder.
        /// </summary>
        public int NewFolderID { get; set; }
        /// <summary>
        /// Destination parent folder ID.
        /// </summary>
        public int DestinationParentFolderID { get; set; }
        /// <summary>
        /// ID of source folder.
        /// </summary>
        public int SourceFolderID { get; set; }
        /// <summary>
        /// Path of new folder.
        /// </summary>
        public string NewFolderPath { get; set; }
        public EdmCmdPostCopyFolder(int mlObjectID1, int mlObjectID2, int mlObjectID3, string mbsStrData1)
            : this()
        {
            NewFolderID = mlObjectID1;
            DestinationParentFolderID = mlObjectID3;
            SourceFolderID = mlObjectID2;
            NewFolderPath = mbsStrData1;
        }
    }
    /// <summary>
    /// A file is about to be deleted.
    /// </summary>
    public struct EdmCmdPreDelete
    {
        /// <summary>
        /// ID of file to delete.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of folder to delete file in.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// Path to file to delete.
        /// </summary>
        public string FilePath { get; set; }
        public EdmCmdPreDelete(int mlObjectID1, int mlObjectID2, string mbsStrData1)
            : this()
        {
            FileID = mlObjectID1;
            FolderID = mlObjectID2;
            FilePath = mbsStrData1;
        }
    }
    /// <summary>
    /// A file has been deleted.
    /// </summary>
    public struct EdmCmdPostDelete
    {
        /// <summary>
        ///ID of file that was deleted.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of folder in which the file was deleted.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// Path to file that was deleted.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Number of folders to which the file is shared.
        /// </summary>
        public int FolderCount { get; set; }
        public EdmCmdPostDelete(int mlObjectID1, int mlObjectID2, string mbsStrData1, int mlLongData1)
            : this()
        {
            FileID = mlObjectID1;
            FolderID = mlObjectID2;
            FilePath = mbsStrData1;
            FolderCount = mlLongData1;
        }
    }
    /// <summary>
    /// A folder is deleted.
    /// </summary>
    public struct EdmCmdPreAndPostDeleteFolder
    {
        /// <summary>
        /// ID of folder to delete.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// Path to folder to delete.
        /// </summary>
        public string FolderPath { get; set; }
        public EdmCmdPreAndPostDeleteFolder(int mlObjectID1, string mbsStrData1)
            : this()
        {
            FolderID = mlObjectID1;
            FolderPath = mbsStrData1;
        }
    }
    /// <summary>
    /// A file is retrieved from the archive to the local hard disk.
    /// </summary>
    public struct EdmCmdPreAndPostGet
    {
        /// <summary>
        /// ID of file to get.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of folder to get file to. (Zero to retrieve a file to a temporary folder.)
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// Destination file path.
        /// </summary>
        public string DestinationFilePath { get; set; }
        /// <summary>
        /// Version number of version to get.
        /// </summary>
        public int VersionNumber { get; set; }
        public EdmCmdPreAndPostGet(int mlObjectID1, int mlObjectID2, string mbsStrData1, int mlLongData1)
            : this()
        {
            FileID = mlObjectID1;
            FolderID = mlObjectID2;
            DestinationFilePath = mbsStrData1;
            VersionNumber = mlLongData1;
        }
    }
    /// <summary>
    /// A file is checked out.
    /// </summary>
    public struct EdmCmdPreAndPostLock
    {
        /// <summary>
        /// ID of file to check out.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of folder to check out file in.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// Path to file.
        /// </summary>
        public string FilePath { get; set; }
        public EdmCmdPreAndPostLock(int mlObjectID1, int mlObjectID2, string mbsStrData1)
            : this()
        {
            FileID = mlObjectID1;
            FolderID = mlObjectID2;
            FilePath = mbsStrData1;
        }
    }
    /// <summary>
    /// A file is moved from one folder to another one.
    /// </summary>
    public struct EdmCmdPreAndPostMove
    {
        /// <summary>
        /// ID of file to move.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of source folder.
        /// </summary>
        public int SourceFolderID { get; set; }
        /// <summary>
        /// ID of destination folder.
        /// </summary>
        public int DestinationFolderID { get; set; }
        /// <summary>
        /// Source file path.
        /// </summary>
        public string SourceFilePath { get; set; }
        /// <summary>
        /// Destination file path.
        /// </summary>
        public string DestinationFilePath { get; set; }
        public EdmCmdPreAndPostMove(int mlObjectID1, int mlObjectID2, int mlObjectID3, string mbsStrData1, string mbsStrData2)
            : this()
        {
            FileID = mlObjectID1;
            SourceFolderID = mlObjectID2;
            DestinationFolderID = mlObjectID3;
            SourceFilePath = mbsStrData1;
            DestinationFilePath = mbsStrData2;
        }
    }
    /// <summary>
    /// A folder is moved from one parent folder to another one.
    /// </summary>
    public struct EdmCmdPreAndPostMoveFolder
    {
        /// <summary>
        /// ID of folder to move.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// ID of source parent folder.
        /// </summary>
        public int SourceParentFolderID { get; set; }
        /// <summary>
        /// ID of destination parent folder.
        /// </summary>
        public int DestinationParentFolderID { get; set; }
        /// <summary>
        /// Source folder path.
        /// </summary>
        public string SourceFolderPath { get; set; }
        /// <summary>
        /// Destination folder path.
        /// </summary>
        public string DestinationFolderPath { get; set; }
        public EdmCmdPreAndPostMoveFolder(int mlObjectID1, int mlObjectID2, int mlObjectID3, string mbsStrData1, string mbsStrData2)
            : this()
        {
            FolderID = mlObjectID1;
            SourceParentFolderID = mlObjectID2;
            DestinationParentFolderID = mlObjectID3;
            SourceFolderPath = mbsStrData1;
            DestinationFolderPath = mbsStrData2;
        }
    }
    /// <summary>
    /// A file is renamed.
    /// </summary>
    public struct EdmCmdPreAndPostRename
    {
        /// <summary>
        /// ID of file to rename.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of the file's parent folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// Old file name.
        /// </summary>
        public string OldFileName { get; set; }
        /// <summary>
        /// New file name.
        /// </summary>
        public string NewFileName { get; set; }
        public EdmCmdPreAndPostRename(int mlObjectID1, int mlObjectID2, string mbsStrData1, string mbsStrData2)
            : this()
        {
            FileID = mlObjectID1;
            ParentFolderID = mlObjectID2;
            OldFileName = mbsStrData1;
            NewFileName = mbsStrData2;
        }
    }
    /// <summary>
    /// A folder is renamed.
    /// </summary>
    public struct EdmCmdPreAndPostRenameFolder
    {
        /// <summary>
        /// ID of folder to rename.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// ID of the folder's parent folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// Old folder name.
        /// </summary>
        public string OldFolderName { get; set; }
        /// <summary>
        /// New folder name.
        /// </summary>
        public string NewFolderName { get; set; }
        public EdmCmdPreAndPostRenameFolder(int mlObjectID1, int mlObjectID2, string mbsStrData1, string mbsStrData2)
            : this()
        {
            FolderID = mlObjectID1;
            ParentFolderID = mlObjectID2;
            OldFolderName = mbsStrData1;
            NewFolderName = mbsStrData2;
        }
    }
    /// <summary>
    /// A file is shared from one folder to another one.
    /// </summary>
    public struct EdmCmdPreAndPostShare
    {
        /// <summary>
        /// ID of file to share.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of folder to share file from.
        /// </summary>
        public int ShareFromFolderID { get; set; }
        /// <summary>
        /// ID of folder to share file to.
        /// </summary>
        public int ShareToFolderID { get; set; }
        /// <summary>
        /// Source file path.
        /// </summary>
        public string SourceFilePath { get; set; }
        public EdmCmdPreAndPostShare(int mlObjectID1, int mlObjectID2, int mlObjectID3, string mbsStrData1)
            : this()
        {
            FileID = mlObjectID1;
            ShareFromFolderID = mlObjectID2;
            ShareToFolderID = mlObjectID3;
            SourceFilePath = mbsStrData1;
        }
    }
    /// <summary>
    /// The user changes the workflow state of a file.
    /// </summary>
    public struct EdmCmdPreAndPostState
    {
        /// <summary>
        /// ID of file to change state on.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of the file's parent folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// ID of the transition (state change) to perform.
        /// </summary>
        public int TransitionID { get; set; }
        /// <summary>
        /// ID of user that performs the state change.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Path to file.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Name of the destination state.
        /// </summary>
        public string DestinationStateName { get; set; }
        /// <summary>
        /// Source state ID.
        /// </summary>
        public int SourceStateID { get; set; }
        /// <summary>
        /// Destination state ID.
        /// </summary>
        public int DestinationStateID { get; set; }
        public EdmCmdPreAndPostState(int mlObjectID1, int mlObjectID2, int mlObjectID3, int mlObjectID4, string mbsStrData1,
            string mbsStrData2, int mlLongData1, int mlLongData2)
            : this()
        {
            FileID = mlObjectID1;
            ParentFolderID = mlObjectID2;
            TransitionID = mlObjectID3;
            UserID = mlObjectID4;
            FilePath = mbsStrData1;
            DestinationStateName = mbsStrData2;
            SourceStateID = mlLongData1;
            DestinationStateID = mlLongData2;
        }
    }
    /// <summary>
    /// The user runs the command Undo check out on a file. (This is the same as check in on an unmodified file.)
    /// </summary>
    public struct EdmCmdPreAndPostUndoLock
    {
        /// <summary>
        /// ID of file to perform undo check out on.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of the file's parent folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// Path to file.
        /// </summary>
        public string FilePath { get; set; }
        public EdmCmdPreAndPostUndoLock(int mlObjectID1, int mlObjectID2, string mbsStrData1)
            : this()
        {
            FileID = mlObjectID1;
            ParentFolderID = mlObjectID2;
            FilePath = mbsStrData1;
        }
    }
    /// <summary>
    /// The user runs check in on a modified file. (check in on unmodified files results in an Undo check out operation.)
    /// </summary>
    public struct EdmCmdPreAndPostUnlock
    {
        /// <summary>
        /// ID of file to check in.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of the file's parent folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// Path to file.
        /// </summary>
        public string FilePath { get; set; }
        public EdmCmdPreAndPostUnlock(int mlObjectID1, int mlObjectID2, string mbsStrData1)
            : this()
        {
            FileID = mlObjectID1;
            ParentFolderID = mlObjectID2;
            FilePath = mbsStrData1;
        }
    }
    /// <summary>
    /// New serial number(s) should be generated by your add-in.
    /// </summary>
    public struct EdmCmdSerialNo
    {
        /// <summary>
        /// ID of file to generate serial number for. (Zero if not generated for a file.)
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// ID of the file's parent folder.
        /// </summary>
        public int ParentFolderID { get; set; }
        /// <summary>
        /// ID of the file data card.
        /// </summary>
        public int DataCardID { get; set; }
        /// <summary>
        /// ID of the control in the file data card.
        /// </summary>
        public int ControlID { get; set; }
        /// <summary>
        /// Return the generated serial number here. (C++ users must allocate the string with the Win32 function SysAllocString.)
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// Path to file. This will be a folder path if the serial number is created for the template manager as part of the folder name.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Name of configuration.
        /// </summary>
        public string ConfigurationName { get; set; }
        /// <summary>
        /// Serial number counter value.
        /// </summary>
        public int SerialNumberCounterValue { get; set; }
        public EdmCmdSerialNo(int mlObjectID1, int mlObjectID2, int mlObjectID3, int mlObjectID4, string mbsStrData1,
            string mbsStrData2, string mbsStrData3, int mlLongData1)
            : this()
        {
            FileID = mlObjectID1;
            ParentFolderID = mlObjectID2;
            DataCardID = mlObjectID3;
            ControlID = mlObjectID4;
            SerialNumber = mbsStrData1;
            FilePath = mbsStrData2;
            ConfigurationName = mbsStrData3;
            SerialNumberCounterValue = mlLongData1;
        }
    }
    #endregion structs
}
