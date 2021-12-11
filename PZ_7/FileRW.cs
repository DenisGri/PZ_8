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

namespace PZ_8;

public static class FileRW
{
    public static ObservableCollection<Student> ReadFromFile()
    {
        var openFileDialog = new OpenFileDialog
        {
            Multiselect = false,
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() != true) return null;
        var path = Path.GetFullPath(openFileDialog.FileName);
        try
        {
            var serializer = new JsonSerializer();
            using var sr = new StreamReader(path);
            using JsonReader reader = new JsonTextReader(sr);

            var data = serializer.Deserialize<ObservableCollection<Student>>(reader);

            return data;
        }
        catch (Exception e)
        {
            MessageBox.Show("Ошибка чтения выбранного файла");
            return null;
        }
    }

    public static void WriteToFile(ObservableCollection<Student> oData)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            Title = "Students.txt",
            OverwritePrompt = true,
            DefaultExt = ".txt"
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            var path = Path.GetFullPath(saveFileDialog.FileName);
            var serializer = new JsonSerializer();
            using var sw = new StreamWriter(path);
            using JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, oData);
        }
    }
}