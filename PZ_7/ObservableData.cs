﻿using System;
using System.Collections.ObjectModel;

namespace PZ_7;

[Serializable]
public class ObservableData
{
    private ObservableCollection<Student> _students = new();

    public ObservableCollection<Student> Students
    {
        get => _students;
        set => _students = value;
    }

}