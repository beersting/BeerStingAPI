using System.Collections.Generic;
using System.IO;
using BeerSting.Api.Enums;

/**
 * 
 	  @author BeerSting <br>
 	     * <b>The MIT License (MIT) Copyright: </b><br>
		 * Copyright (c) 2017, BeerSting<br>
		 * 
		 * <b>Create by: </b><br>
		 * Yoottapong Wongwiwut<br>  
		 * 
		 * <b>Create Date: </b><br>
		 *  July 07 2017<br>
		 * 
		 * <b>Email: </b><br>
		 * <A href="mailto:beer.sting@gmail.com">beer.sting@gmail.com</A><br> 
	  @version 1.0
 * 
 */

namespace BeerSting.Api.Function
{

    public class IOManager
    {

        public static void createDirectory(string fullname)
        {
            string path = fullname;
            if (getName(fullname).IndexOf(".") > -1)
            {
                path = Path.GetDirectoryName(fullname);
            }
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }

        public static bool Exists(string fullname)
        {
            // return ((Path.GetDirectoryName(fullname)) ? File.Exists(fullname) : Directory.Exists(fullname));
            return File.Exists(fullname);
        }

        public static void delete(string fullname)
        {
            if (Exists(fullname))
            {
                File.Delete(fullname);
            }
        }

        //public      bool deleteDirectoryAndAllFile(string path)
        //{
        //    return deleteDirectoryAndAllFile(new File(path));
        //}

        //public      bool deleteDirectoryAndAllFile(File path)
        //{
        //      if (path.exists())
        //      {
        //        File[] files = path.listFiles();
        //        if (files == null)
        //        {
        //            return true;
        //        }
        //        for (int i = 0; i < files.Length; i++)
        //        {
        //           if (files[i].Directory)
        //           {
        //               deleteDirectoryAndAllFile(files[i]);
        //           }
        //           else
        //           {
        //             files[i].delete();
        //           }
        //        }
        //      }
        //      return (path.delete());
        //}

        public static string getPath(string fullname)
        {
            string path = null;
            int index = fullname.LastIndexOf('\\');
            if (index != -1)
            {
                path = fullname.Substring(0, index);
            }
            index = fullname.LastIndexOf('/');
            if (index != -1)
            {
                path = fullname.Substring(0, index);
            }
            return path;
        }

        public static string getName(string fullname)
        {
            return Path.GetFileName(fullname);
        }

        public static string GetFileNameWithoutExtension(string fullname)
        {
            return Path.GetFileNameWithoutExtension(fullname);
        }

        public static string[] getName(params string[] fullname)
        {
            string[] name = new string[fullname.Length];
            for (int i = 0; i < fullname.Length; i++)
            {
                name[i] = Path.GetFileName(fullname[i]);
            }
            return name;
        }

        public static string getNameOnly(string fullname)
        {
            string nameOnly = null;
            string name = getName(fullname);
            string type = getExtension(fullname);
            nameOnly = name.Substring(0, name.LastIndexOf(type) - 1);
            return nameOnly;
        }

        public static string getExtension(string fullname)
        {
            return Path.GetExtension(fullname);
        }

        public static string getType(string fullname)
        {
            return getExtension(fullname);
        }

        // <!-- Check file type -->
        public static bool? checkType(string filetype, FileType type)
        {
            if (filetype == null)
            {
                filetype = "";
            }
            bool chk = false;
            switch (type)
            {
                case FileType.Picture:
                    if (filetype.Equals("image/pjpeg") | filetype.Equals("image/jpeg") | filetype.Equals("image/jpg") | filetype.Equals("image/png") | filetype.Equals("image/x-png") | filetype.Equals("image/gif") | filetype.Equals("image/tif") | filetype.Equals("image/bmp"))
                    {
                        chk = true;
                    }
                    break;
                case FileType.Document:
                    //...
                    break;
                case FileType.Program:
                    //...
                    break;
                case FileType.Byte:
                    chk = true;
                    break;
                case FileType.All:
                    chk = true;
                    break;
                default:
                    break;
            }

            return chk;
        }

        // public      string[] get(string dir, SystemType systemType, Node node)
        // {
        //       IList<string> result = this.get(new File(dir), systemType, node);
        //       return (string[])result.ToArray();
        // }

        // public      IList<string> get(File dir, SystemType systemType, Node node)
        // {
        //     IList<string> result = new List<string>();
        //     if (dir.Directory)
        //     {
        //           string[] children = dir.list();
        //           for (int i = 0; i < children.Length; i++)
        //           {
        //               if (System.IO.Directory.Exists(dir.AbsolutePath + "\\" + children[i]))
        //               {
        //                if (systemType == SystemType.Folder || systemType == SystemType.All)
        //                {
        //                    result.Add(dir.AbsolutePath + "\\" + children[i]);
        //                }
        //                if (node == Node.All)
        //                {
        //                    result.AddRange(get(new File(dir, children[i]),systemType,node));
        //                }
        //               }
        //          else
        //          {
        //              if (systemType == SystemType.File || systemType == SystemType.All)
        //              {
        //                  result.Add(dir.AbsolutePath + "\\" + children[i]);
        //              }
        //          }
        //           }
        //     }
        //     return result;
        // }

        //public      void write(string text, string fullname, bool append)
        //{
        //       write(text.GetBytes(), fullname, append);
        //}


        /*  public void write(String text,String fullname,boolean append){
                try {
                    String path = getPath(fullname);
                    createDirectory(path);
                    BufferedWriter writer = new BufferedWriter(new FileWriter(fullname,append)) ;
                    writer.append(text);
                    writer.close() ;
                } catch (IOException e) {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }
             }*/

        //public      void write(sbyte[] bytes, string fullname, bool append)
        //{
        //          try
        //          {
        //              string path = getPath(fullname);
        //              createDirectory(path);
        //              BufferedOutputStream bufferedOutput = new BufferedOutputStream(new FileOutputStream(fullname,append));
        //              bufferedOutput.write(bytes);
        //              bufferedOutput.flush();
        //              bufferedOutput.close();
        //          }
        //          catch (IOException e)
        //          {
        //              // TODO Auto-generated catch block
        //              Console.WriteLine(e.ToString());
        //              Console.Write(e.StackTrace);
        //          }
        //}

        //public      string read(string fullname)
        //{
        //    return toText(fullname);
        //}

        //public      string toText(string fullname)
        //{
        //      sbyte[] bytes = null;
        //      File file = new File(fullname);
        //      if (file.exists() && file.File)
        //      {
        //          try
        //          {
        //              FileInputStream fi = new FileInputStream(file); //create FileInputStream which obtains input bytes from a file in a file system
        //              BufferedInputStream buffer = new BufferedInputStream(fi);
        //              DataInputStream data = new DataInputStream(buffer);
        //              bytes = new sbyte[(int) file.length()];
        //              data.readFully(bytes);
        //              fi.close();
        //              buffer.close();
        //              data.close();
        //          }
        //          catch (IOException e)
        //          {
        //              // TODO Auto-generated catch block
        //              Console.WriteLine(e.ToString());
        //              Console.Write(e.StackTrace);
        //          }
        //      }
        //     // return text;
        //      return StringHelperClass.NewString(bytes);
        //}

        //public      IList<string> toTextLine(string fullname)
        //{
        //  IList<string> line = new List<string>();
        //  File file = new File(fullname);
        //  try
        //  {
        //      FileInputStream fi = new FileInputStream(file);
        //      BufferedInputStream buffer = new BufferedInputStream(fi);
        //      DataInputStream data = new DataInputStream(buffer);
        //      string text = null;
        //      while ((text = data.readLine()) != null)
        //      {
        //          line.Add(text);
        //      }
        //      fi.close();
        //      buffer.close();
        //      data.close();
        //  }
        //  catch (IOException e)
        //  {
        //      // TODO Auto-generated catch block
        //      Console.WriteLine(e.ToString());
        //      Console.Write(e.StackTrace);
        //  }
        //  return line;
        //}


        //public      sbyte[] toByte(string fullname)
        //{
        //     ByteArrayOutputStream bos = null;
        //      File file = new File(fullname);
        //      if (file.exists() && file.File)
        //      {
        //     try
        //     {
        //          FileInputStream fis = new FileInputStream(file); //create FileInputStream which obtains input bytes from a file in a file system
        //          //FileInputStream is meant for reading streams of raw bytes such as image data. For reading streams of characters, consider using FileReader.
        //          //System.out.println(file.exists() + "!!");
        //          //InputStream in = resource.openStream();
        //          bos = new ByteArrayOutputStream();
        //          sbyte[] buf = new sbyte[1024];
        //              for (int readNum; (readNum = fis.read(buf)) != -1;)
        //              {
        //                  bos.write(buf, 0, readNum); //no doubt here is 0
        //                  //Writes len bytes from the specified byte array starting at offset off to this byte array output stream.
        //                  //System.out.println("read " + readNum + " bytes,");
        //              }
        //     }
        //          catch (IOException e)
        //          {
        //              // TODO Auto-generated catch block
        //              Console.WriteLine(e.ToString());
        //              Console.Write(e.StackTrace);
        //          }
        //      }
        //      return bos.toByteArray();
        //}


    }

}