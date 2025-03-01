using System;
using System.Collections.Generic;

class Student {
    public string Name { get; }
    public Course EnrolledCourse { get; private set; }

    public Student(string name) {
        Name = name;
    }

    // Separating enrollment from constructor so that 
    // the studet does get created before enrolling in a course
    public void EnrollInCourse(Course course) {
        EnrolledCourse = course;
        Console.WriteLine($"Student {Name} is enrolling in {course.CourseName}.");
        course.AddStudent(this); 
    }
}

class Course {
    public string CourseName { get; }
    public List<Student> Students { get; } = new List<Student>();

    public Course(string courseName) {
        CourseName = courseName;
        Console.WriteLine($"Course {CourseName} created.");
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
        Student student = new Student("Ahmed"); /

        // Main now uses sequential creation of objects
        // and enrollment, preventing cyclic constructor calls.
        Console.WriteLine("\nEnrolling in the course");
        student.EnrollInCourse(course);
    }
}