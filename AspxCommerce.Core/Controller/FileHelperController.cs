/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 20011-2012 by AspxCommerce
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.IO;
using SageFrame.Web;
using System.Web.Hosting;
using System.ServiceModel.Activation;

namespace AspxCommerce.Core
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class FileHelperController
    {
        public FileHelperController()
        {
        }

        public string MoveFileToModuleFolder(string tempFolder, string prevFile, string imageVar, int largeThumbNailImageSize, int mediumThumbNailImageSize, int smallThumbNailImageSize, string destinationFolder, int ID, string imgPreFix)
        {
            string destinationFile = string.Empty;
            string destinationLargeFile = string.Empty;
            string destinationMediumFile = string.Empty;
            string destinationSmallFile = string.Empty;

            string strRootPath = HostingEnvironment.MapPath("~/");
            destinationFolder = strRootPath + destinationFolder;
            if (imageVar != "")
            {
                string fileExt = imageVar.Substring(imageVar.LastIndexOf("."));
                Random rnd = new Random();
                string fileName = imgPreFix + ID + '_' + rnd.Next(111111, 999999).ToString() + fileExt;
                string sourceFile = strRootPath + imageVar;

                try
                {
                    if (File.Exists(sourceFile))
                    {
                        // To move a file or folder to a new location:
                        if (!Directory.Exists(destinationFolder))
                        {
                            Directory.CreateDirectory(destinationFolder);
                        }

                        destinationFile = destinationFolder + fileName;

                        if (sourceFile != destinationFile)
                        {
                            if (prevFile != "")
                            {
                                string prevFileName = prevFile.Substring(prevFile.LastIndexOf("\\") + 1);
                                string prevExistingFile = destinationFolder + prevFileName;
                                destinationLargeFile = destinationFolder + "Large\\" + prevFileName;
                                destinationMediumFile = destinationFolder + "Medium\\" + prevFileName;
                                destinationSmallFile = destinationFolder + "Small\\" + prevFileName;

                                if (File.Exists(destinationLargeFile))
                                {
                                    File.Delete(destinationLargeFile);
                                }
                                if (File.Exists(destinationMediumFile))
                                {
                                    File.Delete(destinationMediumFile);
                                }
                                if (File.Exists(destinationSmallFile))
                                {
                                    File.Delete(destinationSmallFile);
                                }
                                if (File.Exists(prevExistingFile))
                                {
                                    File.Delete(prevExistingFile);
                                }
                            }

                            System.IO.File.Move(sourceFile, destinationFile);

                            string vertualUrl0 = destinationFolder + "Large\\";
                            string vertualUrl1 = destinationFolder + "Medium\\";
                            string vertualUrl2 = destinationFolder + "Small\\";

                            if (!Directory.Exists(vertualUrl0))
                                Directory.CreateDirectory(vertualUrl0);
                            if (!Directory.Exists(vertualUrl1))
                                Directory.CreateDirectory(vertualUrl1);
                            if (!Directory.Exists(vertualUrl2))
                                Directory.CreateDirectory(vertualUrl2);

                            vertualUrl0 = vertualUrl0 + fileName;
                            vertualUrl1 = vertualUrl1 + fileName;
                            vertualUrl2 = vertualUrl2 + fileName;

                            string[] imageType = new string[] { "jpg", "jpeg","jpe","gif","bmp","png","ico" };
                            bool isValidImage = false;
                            foreach (string x in imageType)
                            {
                                if (fileExt.Contains(x))
                                {
                                    isValidImage = true;
                                    break;
                                }                                   
                            }

                            if (isValidImage)
                            {
                                PictureManager.CreateThmnail(destinationFile, largeThumbNailImageSize, vertualUrl0);
                                PictureManager.CreateThmnail(destinationFile, mediumThumbNailImageSize, vertualUrl1);
                                PictureManager.CreateThmnail(destinationFile, smallThumbNailImageSize, vertualUrl2);
                            }
                            else
                            {
                                System.IO.File.Copy(destinationFile, vertualUrl0);
                                System.IO.File.Copy(destinationFile, vertualUrl1);
                                System.IO.File.Copy(destinationFile, vertualUrl2);
                            }

                            ////Check previously unsaved files and delete
                            //string tempFolderPath = strRootPath + tempFolder;
                            //DirectoryInfo temDir = new DirectoryInfo(tempFolderPath);
                            //if (temDir.Exists)
                            //{
                            //    FileInfo[] files = temDir.GetFiles();

                            //    foreach (FileInfo file in files)
                            //    {
                            //        if (file.CreationTime.ToShortDateString() != DateTime.Now.ToShortDateString())
                            //        {
                            //            System.IO.File.Delete(file.FullName);
                            //        }
                            //    }
                            //}
                            destinationFile = vertualUrl2;
                        }
                        destinationFile = destinationFile.Replace(strRootPath, "");
                        destinationFile = destinationFile.Replace("\\", "/");
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                if (prevFile != "")
                {
                    try
                    {
                        string prevFileName = prevFile.Substring(prevFile.LastIndexOf("\\") + 1);
                        string prevExistingFile = destinationFolder + prevFileName;
                        destinationLargeFile = destinationFolder + "Large\\" + prevFileName;
                        destinationMediumFile = destinationFolder + "Medium\\" + prevFileName;
                        destinationSmallFile = destinationFolder + "Small\\" + prevFileName;

                        if (File.Exists(destinationLargeFile))
                        {
                            File.Delete(destinationLargeFile);
                        }
                        if (File.Exists(destinationMediumFile))
                        {
                            File.Delete(destinationMediumFile);
                        }
                        if (File.Exists(destinationSmallFile))
                        {
                            File.Delete(destinationSmallFile);
                        }
                        if (File.Exists(prevExistingFile))
                        {
                            File.Delete(prevExistingFile);
                        }
                    }
                    catch
                        (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
            return destinationFile;
        }

        public void FileMover(int itemID, string imgRootPath, string sourceFileCol, string pathCollection, string isActive, string imageType, string description, string displayOrder, string imgPreFix,int itemLargeThumbNailSize,int itemMediumThumbNailSize, int itemSmallThumbNailSize)
        {
            string pathList = string.Empty;
            string[] sourceFileList = sourceFileCol.Split('%');
            string destpath = HostingEnvironment.MapPath("~/" + imgRootPath);
            if (!Directory.Exists(destpath))
            {
                Directory.CreateDirectory(destpath);
            }

            Random randVar = new Random();

            try
            {
                string[] sourceList = pathCollection.Split('%');
                //  DirectoryInfo destDir = new DirectoryInfo(destpath);

                #region FileDelete
                // Delete images from dstination folder of same itemID
                /*  
                                   if (destDir.Exists)
                                   {
                                       FileInfo[] files = GetFilesList(int.Parse(itemID), destDir);

                                       string listFiles = string.Empty;
                                       int j = 0;
                                       if (files.Length != 0)
                                       {
                                           while (j < files.Length)
                                           {

                                               if (files[j].FullName.Contains(".db") || files[j].FullName.Contains(".DB"))
                                               {
                                                   System.IO.File.Delete(files[j].FullName);
                                               }
                                               j++;
                                           }
                                           foreach (FileInfo file in files)
                                           {
                                               foreach (string fileStr in sourceList)
                                               {
                                                   string sourceCol = HostingEnvironment.MapPath("~/" + fileStr);
                                                   string destCol = file.DirectoryName + "\\" + file;
                                                   if (destCol == sourceCol)
                                                   {
                                                       break;
                                                   }
                                                   else
                                                   {
                                                       listFiles += sourceCol + ",";
                                                   }

                                               }
                                           }
                                       }





                                       if (listFiles.Contains(","))
                                       {
                                           listFiles = listFiles.Remove(listFiles.LastIndexOf(","));


                                           string[] delFileCol = listFiles.Split(',');

                                           int count = delFileCol.Length;
                                           int i = 0;
                                           while (i < count)
                                           {
                                               if (File.Exists(delFileCol[i]))
                                               {
                                                   if (delFileCol[i].Contains(".db") || delFileCol[i].Contains(".DB"))
                                                   {
                                                       System.IO.File.Delete(delFileCol[i]);
                                                   }

                                                   else
                                                   {
                                                       string[] path_Splitter = delFileCol[i].ToString().Split('\\');
                                                       int words_length = path_Splitter.Length;
                                                       string[] words_Splitter = path_Splitter[words_length - 1].Split('_');
                                                       int length = words_Splitter.Length;
                                                       if (words_Splitter[length - 2].ToString() == itemID)
                                                       {
                                                           System.IO.File.Delete(delFileCol[i]);
                                                       }
                                                   }
                                               }
                                               i++;
                                           }
                                       }
                                   }*/
                #endregion

                //Move files from source to destination folder
                for (int i = 0; i < sourceFileList.Length; i++)
                {
                    string sourceCol = HostingEnvironment.MapPath("~/" + sourceList[i]);
                    string fileExt = sourceFileList[i].Substring(sourceFileList[i].LastIndexOf("."));
                    string fileName = imgPreFix + itemID + '_' + randVar.Next(111111, 999999).ToString() + fileExt;
                    pathList += imgRootPath + fileName + "%";
                    //TODO:: make only image Name instead Path 
                    sourceFileList[i] = fileName;
                    if (destpath != null)
                    {
                        string destination = Path.Combine(destpath, sourceFileList[i]);
                        if (sourceCol != destination)
                        {
                            if (File.Exists(sourceCol) && !File.Exists(destination))
                            {
                                if (sourceCol != null) File.Copy(sourceCol, destination);
                                //image Thumbnails generates here
                                string vertualUrl0 = destpath + "Large\\";
                                string vertualUrl1 = destpath + "Medium\\";
                                string vertualUrl2 = destpath + "Small\\";

                                if (!Directory.Exists(vertualUrl0))
                                    Directory.CreateDirectory(vertualUrl0);
                                if (!Directory.Exists(vertualUrl1))
                                    Directory.CreateDirectory(vertualUrl1);
                                if (!Directory.Exists(vertualUrl2))
                                    Directory.CreateDirectory(vertualUrl2);

                                vertualUrl0 = vertualUrl0 + fileName;
                                vertualUrl1 = vertualUrl1 + fileName;
                                vertualUrl2 = vertualUrl2 + fileName;

                                string[] imageTypeFiles = new string[] { "jpg", "jpeg", "jpe", "gif", "bmp", "png", "ico" };
                                bool isValidImage = false;
                                foreach (string x in imageTypeFiles)
                                {
                                    if (fileExt.Contains(x))
                                    {
                                        isValidImage = true;
                                        break;
                                    }
                                }

                                if (isValidImage)
                                {
                                    PictureManager.CreateThmnail(destination, itemLargeThumbNailSize, vertualUrl0);
                                    PictureManager.CreateThmnail(destination, itemMediumThumbNailSize, vertualUrl1);
                                    PictureManager.CreateThmnail(destination, itemSmallThumbNailSize, vertualUrl2);
                                }
                                else
                                {
                                    System.IO.File.Copy(destination, vertualUrl0);
                                    System.IO.File.Copy(destination, vertualUrl1);
                                    System.IO.File.Copy(destination, vertualUrl2);
                                }
                            }
                        }
                    }
                }
                if (pathList.Contains("%"))
                {
                    pathList = pathList.Remove(pathList.LastIndexOf("%"));
                }

                //Save to database
                ImageUploaderSqlhandler imageSql = new ImageUploaderSqlhandler();
                imageSql.SaveImageSettings(itemID, pathList, isActive, imageType, description, displayOrder);

                //// delete files created earlier
                //DirectoryInfo temDir = new DirectoryInfo(sourcepath);
                //if (temDir.Exists)
                //{
                //    FileInfo[] files = temDir.GetFiles();
                //    foreach (FileInfo file in files)
                //    {
                //        if (file.CreationTime.ToShortDateString() != DateTime.Now.ToShortDateString())
                //        {
                //            System.IO.File.Delete(file.FullName);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FileInfo[] GetFilesList(int itemID, DirectoryInfo testDir)
        {
            FileInfo[] filCol = testDir.GetFiles();
            return filCol;
        }

        public string MoveFileToSpecificFolder(string tempFolder, string prevFilePath, string newFilePath, string destinationFolder, int ID, string imgPreFix)
        {
            //imagePath = imagePath.Replace("../../", "");
            newFilePath = newFilePath.Replace("/", "\\");

            string strRootPath = HostingEnvironment.MapPath("~/");
            string fileExt = newFilePath.Substring(newFilePath.LastIndexOf("."));
            Random rnd = new Random();
            string fileName = imgPreFix + ID + '_' + rnd.Next(111111, 999999).ToString() + fileExt;

            string sourceFile = strRootPath + newFilePath;
            string destinationFile = string.Empty;
            destinationFolder = strRootPath + destinationFolder;

            try
            {
                if (File.Exists(sourceFile))
                {
                    // To move a file or folder to a new location:
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    destinationFile = destinationFolder + fileName;
                    if (sourceFile != destinationFile)
                    {
                        if (File.Exists(destinationFile))
                        {
                            File.Delete(destinationFile);
                        }
                        System.IO.File.Move(sourceFile, destinationFile);
                        if (prevFilePath != "")
                        {
                            prevFilePath = prevFilePath.Replace("/", "\\");
                            prevFilePath = strRootPath + prevFilePath;
                           // System.IO.File.Delete(prevFilePath);
                        }

                        string vertualUrl0 = destinationFolder + "Large\\";
                        string vertualUrl1 = destinationFolder + "Medium\\";
                        string vertualUrl2 = destinationFolder + "Small\\";

                        if (!Directory.Exists(vertualUrl0))
                            Directory.CreateDirectory(vertualUrl0);
                        if (!Directory.Exists(vertualUrl1))
                            Directory.CreateDirectory(vertualUrl1);
                        if (!Directory.Exists(vertualUrl2))
                            Directory.CreateDirectory(vertualUrl2);

                        vertualUrl0 = vertualUrl0 + fileName;
                        vertualUrl1 = vertualUrl1 + fileName;
                        vertualUrl2 = vertualUrl2 + fileName;

                        string[] imageType = new string[] { "jpg", "jpeg", "jpe", "gif", "bmp", "png", "ico" };
                        bool isValidImage = false;
                        foreach (string x in imageType)
                        {
                            if (fileExt.Contains(x))
                            {
                                isValidImage = true;
                                break;
                            }
                        }

                        if (isValidImage)
                        {
                            PictureManager.CreateThmnail(destinationFile, 320, vertualUrl0);
                            PictureManager.CreateThmnail(destinationFile, 240, vertualUrl1);
                            PictureManager.CreateThmnail(destinationFile, 125, vertualUrl2);
                        }
                        else
                        {
                            System.IO.File.Copy(destinationFile, vertualUrl0);
                            System.IO.File.Copy(destinationFile, vertualUrl1);
                            System.IO.File.Copy(destinationFile, vertualUrl2);
                        }
                        ////Check previously unsaved files and delete
                        //string tempFolderPath = strRootPath + tempFolder;
                        //DirectoryInfo temDir = new DirectoryInfo(tempFolderPath);
                        //if (temDir.Exists)
                        //{
                        //    FileInfo[] files = temDir.GetFiles();

                        //    foreach (FileInfo file in files)
                        //    {
                        //        if (file.CreationTime.ToShortDateString() != DateTime.Now.ToShortDateString())
                        //        {
                        //            System.IO.File.Delete(file.FullName);
                        //        }
                        //    }
                        //}
                    }
                    destinationFile = destinationFile.Replace(strRootPath, "");
                    destinationFile = destinationFile.Replace("\\", "/");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return destinationFile;
        }

        public string MoveFileToDownlodableItemFolder(string tempFolder, string downloadItemsValue, string destinationFolder, int itemID, string filePreFix)
        {
            string[] individualRow = downloadItemsValue.Split('%');
            destinationFolder = destinationFolder.Replace("/", "\\");

            string title = individualRow[0];
            int maxDownload = Convert.ToInt32(individualRow[1]);
            string isSharable = individualRow[2];
            string fileSample = individualRow[3];
            string newFileSamplePath = individualRow[4];
            string fileActual = individualRow[5];
            string newFileActualPath = individualRow[6];
            int displayOrder = Convert.ToInt32(individualRow[7]);

            string strRootPath = HostingEnvironment.MapPath("~/");
            string fileSampleName = string.Empty;
            string fileActualName = string.Empty;
            string sourceFileSample = string.Empty;
            string sourceFileActual = string.Empty;
            string destinationFileSample = string.Empty;
            string destinationFileActual = string.Empty;
            Random rnd = new Random();

            if (newFileSamplePath != "")
            {
                newFileSamplePath = newFileSamplePath.Replace("/", "\\");
                fileSampleName = newFileSamplePath.Substring(newFileSamplePath.LastIndexOf("\\"));
                fileSampleName = filePreFix + '_' + rnd.Next(111111, 999999).ToString() + '_' + fileSampleName.Replace("\\", "");
                sourceFileSample = strRootPath + newFileSamplePath;
            }
            else
            {
                fileSampleName = fileSample;
            }

            if (newFileActualPath != "")
            {
                newFileActualPath = newFileActualPath.Replace("/", "\\");
                fileActualName = newFileActualPath.Substring(newFileActualPath.LastIndexOf("\\"));
                fileActualName = filePreFix + '_' + rnd.Next(111111, 999999).ToString() + '_' + fileActualName.Replace("\\", "");
                sourceFileActual = strRootPath + newFileActualPath;
            }
            else
            {
                fileActualName = fileActual;
            }

            destinationFolder = strRootPath + destinationFolder;
            fileSample = destinationFolder + fileSample;
            fileActual = destinationFolder + fileActual;

            try
            {
                if (File.Exists(sourceFileSample))
                {
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }
                    destinationFileSample = destinationFolder + fileSampleName;

                    if (sourceFileSample != destinationFileSample)
                    {
                        if (File.Exists(fileSample))
                        {
                            File.Delete(fileSample);
                        }
                        System.IO.File.Move(sourceFileSample, destinationFileSample);
                    }
                    //destinationFileSample = destinationFileSample.Replace(strRootPath, "");
                    //destinationFileSample = destinationFileSample.Replace("\\", "/");
                }
                //else { }
                if (File.Exists(sourceFileActual))
                {
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }
                    destinationFileActual = destinationFolder + fileActualName;

                    if (sourceFileActual != destinationFileActual)
                    {
                        if (File.Exists(fileActual))
                        {
                            File.Delete(fileActual);
                        }
                        System.IO.File.Move(sourceFileActual, destinationFileActual);
                    }
                    //destinationFileActual = destinationFileActual.Replace(strRootPath, "");
                    //destinationFileActual = destinationFileActual.Replace("\\", "/");
                }
            }
            catch (Exception)
            {
                //throw ex;
                if (File.Exists(fileSample))
                {
                    File.Delete(fileSample);
                }
                if (File.Exists(fileActual))
                {
                    File.Delete(fileActual);
                }
            }

            string valueUploadedContents = title + '%' + maxDownload + '%' + isSharable + '%' + fileSampleName + '%' + fileActualName + '%' + displayOrder;

            return valueUploadedContents;
        }
  
    }
}
