using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace PZ_7;

public static class FileRW
{
    public static ObservableCollection<Student> ReadFromFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Multiselect = false,
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() != true) return null;
        var path = Path.GetFullPath(openFileDialog.FileName);
        try
        {
            JsonSerializer serializer = new JsonSerializer();
            using StreamReader sr = new StreamReader(path);
            using JsonReader reader = new JsonTextReader(sr);

            var data = serializer.Deserialize<ObservableCollection<Student>>(reader);

            return data;
        }
        catch (Exception e)
        {
            MessageBox.Show("Ошибка чтения выбранного файла");
            return null;
        }

        return null;
    }

    public static void WriteToFile(ObservableCollection<Student> oData)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            Title = "Students.txt",
            OverwritePrompt = true,
            DefaultExt = ".txt"
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            var path = Path.GetFullPath(saveFileDialog.FileName);
            JsonSerializer serializer = new JsonSerializer();
            using StreamWriter sw = new StreamWriter(path);
            using JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, oData);
        }
    }
}