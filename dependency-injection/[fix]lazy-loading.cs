using System;
using System.Collections.Generic;

class Student {
    public string Name { get; }

    public Lazy<Course> LEnrolledCourse;
    public Course EnrolledCourse => LEnrolledCourse.Value; 

    public Student(string name, Course course) {
        Name = name;

        Console.WriteLine($"Lazy loading: Enrolling {Name} in {course.CourseName}.");
        LEnrolledCourse = new Lazy<Course>(() => {
            course.AddStudent(this);
            return course;
        });
        Console.WriteLine($"Student {Name} created, but course enrollment is delayed.");
    }
}

class Course {
    public string CourseName { get; }
    public List<Student> Students { get; } = new List<Student>();

    public Course(string courseName) {
        CourseName = courseName;
    }

    public void AddStudent(Student student) {
        // check to prevent duplicate entry of a student in a course
        if (!Students.Contains(student)) {
            Students.Add(student);
            Console.WriteLine($"{student.Name} added to the course {CourseName}.");
        }
    }
}

class Program {
    static void Main() {
        Console.WriteLine("Creating Course...");
        Course course = new Course("CS50");

        Console.WriteLine("\nCreating Student...");
        Student student = new Student("Ahmed", course);
        
        Console.WriteLine("\nAccessing Student's Course (Triggers Lazy Loading)");
        Console.WriteLine($"Student {student.Name} is now enrolled in {student.EnrolledCourse.CourseName}.");
    }
}