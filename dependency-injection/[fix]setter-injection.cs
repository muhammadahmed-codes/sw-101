using System;
using System.Collections.Generic;

class Student {
    public string Name { get; }
    
    public Course _EnrolledCourse;

    public Course EnrolledCourse { 
        get { return _EnrolledCourse; }

        set { 
            if (_EnrolledCourse == null) {
                _EnrolledCourse = value;
                Console.WriteLine($"Student {Name} is enrolling in {value.CourseName}.");
                _EnrolledCourse.AddStudent(this);
            }
        } 
    }

    public Student(string name) {
        Name = name;
        Console.WriteLine($"Student {Name} created.");
    }
}

class Course {
    public string CourseName { get; }
    public List<Student> Students { get; } = new List<Student>();

    public Course(string courseName) {
        CourseName = courseName;
    }

    public void AddStudent(Student student) {
        if (!Students.Contains(student)) {
            Console.WriteLine($"Adding {student.Name} to the course {CourseName}.");
            Students.Add(student);
        }
    }
}

class Program {
    static void Main() {
        Console.WriteLine("Creating Course...");
        Course course = new Course("CS50");

        Console.WriteLine("\nCreating Student...");
        Student student = new Student("Ahmed"); 

        Console.WriteLine("\nSetting Student's Course (Setter Injection)...");
        student.EnrolledCourse = course;
    }
}