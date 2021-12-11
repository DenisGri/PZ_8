using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace PZ_7
{
    public readonly struct Student
    {
        private readonly string _fio;
        private readonly string _group;
        private readonly int[] _scores = new int[5];

        public Student(string fio, string group, int[] scores)
        {
            _fio = fio;
            _group = group;
            _scores = scores;
        }

        public string? GetFio => _fio;
        public string? GetGroup => _group;
        public string? GetScores => string.Join(",", _scores);

        public static List<Student> StudentList { get; set; } = GetStudents();

        public static List<Student> GetStudents()
        {
            List<Student> ss = new List<Student>();
            Student student1 = new Student("Kent C.K.", "55555", new[] {6, 7, 8, 9, 10});
            Student student2 = new Student("Kent K.C.", "12345", new[] {1, 2, 3, 4, 5});
            ss.Add(student1);
            ss.Add(student2);
            ss.Add(student1);
            ss.Add(student1);
            ss.Add(student2);
            StudentList = ss;
            return ss;
        }

        public static List<Student> AddStudent(Student student)
        {
            StudentList.Add(student);

            return StudentList;
        }

        public List<Student> ShowHighScoreStudents(List<Student> rawList)
        {
            var highScoreStudents = new List<Student>();
            if (highScoreStudents == null) throw new ArgumentNullException(nameof(highScoreStudents));
            highScoreStudents.AddRange(rawList.Where(student => student._scores.Any(score => score >= 9)));

            return highScoreStudents;
        }
    }
}