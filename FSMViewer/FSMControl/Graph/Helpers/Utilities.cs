﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMControl
{
    public class Utilities
    {
        /// <summary>
        /// Froms the XML string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlData">The XML data.</param>
        /// <returns></returns>
        public static T FromXmlString<T>(string xmlData)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StreamReader streamReader = new StreamReader(xmlData);
            return (T)xmlSerializer.Deserialize(streamReader);
        }

        public static string GetPath(string title)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            bool? userClickedOK = openFileDialog.ShowDialog();
            if (userClickedOK == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        public static string SavePath(string title)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = title;
            
            bool? userClickedOK= save.ShowDialog();

            if (userClickedOK == true)
            {
                return save.FileName;
            }
            return null;
        }
        
    }
}