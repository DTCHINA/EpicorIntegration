using System;
using System.Collections.Generic;
using System.Text;

using EdmLib;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace EPDMDebugApp
{
    [Guid("A20AF619-E2CA-4694-8F35-B873D465A016"), ComVisible(true)]
    class edmAddin : IEdmAddIn5
    {
        void EdmLib.IEdmAddIn5.GetAddInInfo(ref EdmAddInInfo poInfo, IEdmVault5 poVault, IEdmCmdMgr5 poCmdMgr)
        {
            //Fill in the AddIn's description
            poInfo.mbsAddInName = "Monitor";
            poInfo.mbsCompany = "SolidWorks";
            poInfo.mbsDescription = "Addin";
            poInfo.mlAddInVersion = 1;

            //Minimum Conisio version needed for .Net Add-Ins is 6.4
            poInfo.mlRequiredVersionMajor = 6;
            poInfo.mlRequiredVersionMinor = 4;

            poCmdMgr.AddCmd(1, "Test Menu from SWEPDMAddin2", (int)EdmMenuFlags.EdmMenu_Nothing, "", "", 0, 0);

            //uncomment this line to add all hooks
            AddAllHooks(poCmdMgr);
        }


        void EdmLib.IEdmAddIn5.OnCmd(ref EdmCmd poCmd, ref System.Array ppoData)
        {
            Debug.Print("Command Type: " + poCmd.meCmdType.ToString() + "\n  " + System.DateTime.Now.ToString());

            IEdmVault5 edmVault = poCmd.mpoVault as IEdmVault5;

            try
            {
                switch (poCmd.meCmdType)
                {
                    case EdmCmdType.EdmCmd_CardButton:
                        break;
                    case EdmCmdType.EdmCmd_CardInput:
                        break;
                    case EdmCmdType.EdmCmd_CardListSrc:
                        break;
                    case EdmCmdType.EdmCmd_InstallAddIn:
                        break;
                    case EdmCmdType.EdmCmd_Menu:
                        edmVault.MsgBox(0, " SWEPDMAddin2", EdmMBoxType.EdmMbt_OKOnly, "SolidWorks EPDM Addin");
                        break;
                    case EdmCmdType.EdmCmd_PostAdd:
                        break;
                    case EdmCmdType.EdmCmd_PostAddFolder:
                        break;
                    case EdmCmdType.EdmCmd_PostCopy:
                        break;
                    case EdmCmdType.EdmCmd_PostCopyFolder:
                        break;
                    case EdmCmdType.EdmCmd_PostDelete:
                        break;
                    case EdmCmdType.EdmCmd_PostDeleteFolder:
                        break;
                    case EdmCmdType.EdmCmd_PostGet:
                        break;
                    case EdmCmdType.EdmCmd_PostLock:
                        break;
                    case EdmCmdType.EdmCmd_PostMove:
                        break;
                    case EdmCmdType.EdmCmd_PostMoveFolder:
                        break;
                    case EdmCmdType.EdmCmd_PostRename:
                        break;
                    case EdmCmdType.EdmCmd_PostRenameFolder:
                        break;
                    case EdmCmdType.EdmCmd_PostShare:
                        break;
                    case EdmCmdType.EdmCmd_PostState:
                        break;
                    case EdmCmdType.EdmCmd_PostUndoLock:
                        break;
                    case EdmCmdType.EdmCmd_PostUnlock:
                        break;
                    case EdmCmdType.EdmCmd_PreAdd:
                        break;
                    case EdmCmdType.EdmCmd_PreAddFolder:
                        break;
                    case EdmCmdType.EdmCmd_PreCopy:
                        break;
                    case EdmCmdType.EdmCmd_PreCopyFolder:
                        break;
                    case EdmCmdType.EdmCmd_PreDelete:
                        break;
                    case EdmCmdType.EdmCmd_PreDeleteFolder:
                        break;
                    case EdmCmdType.EdmCmd_PreGet:
                        break;
                    case EdmCmdType.EdmCmd_PreLock:
                        break;
                    case EdmCmdType.EdmCmd_PreMove:
                        break;
                    case EdmCmdType.EdmCmd_PreMoveFolder:
                        break;
                    case EdmCmdType.EdmCmd_PreRename:
                        break;
                    case EdmCmdType.EdmCmd_PreRenameFolder:
                        break;
                    case EdmCmdType.EdmCmd_PreShare:
                        break;
                    case EdmCmdType.EdmCmd_PreState:
                        break;
                    case EdmCmdType.EdmCmd_PreUndoLock:
                        break;
                    case EdmCmdType.EdmCmd_PreUnlock:
                        break;
                    case EdmCmdType.EdmCmd_SerialNo:
                        break;
                    case EdmCmdType.EdmCmd_UninstallAddIn:
                        break;
                    default:
                        break;
                }
            }
            catch (COMException exp)
            {
                string errorName, errorDesc;
                edmVault.GetErrorString(exp.ErrorCode, out errorName, out errorDesc);
                edmVault.MsgBox(0, errorDesc, EdmMBoxType.EdmMbt_OKOnly, errorName);
            }
        }

        void AddAllHooks(IEdmCmdMgr5 poCmdMgr)
        {
            poCmdMgr.AddHook(EdmCmdType.EdmCmd_CardButton, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_CardInput, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_CardListSrc, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_InstallAddIn, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_Menu, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostAdd, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostAddFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostCopy, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostCopyFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostDelete, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostDeleteFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostGet, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostLock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostMove, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostMoveFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostRename, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostRenameFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostShare, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostState, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostUndoLock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostUnlock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreAdd, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreAddFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreCopy, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreCopyFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreDelete, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreDeleteFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreGet, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreLock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreMove, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreMoveFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreRename, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreRenameFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreShare, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreState, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreUndoLock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreUnlock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_SerialNo, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_UninstallAddIn, null);
        }
    }
}
