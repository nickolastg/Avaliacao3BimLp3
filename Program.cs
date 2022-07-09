using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Repositories;
using Avaliacao3BimLp3.Models;

var databaseConfig = new DatabaseConfig();

var DatabaseSetup = new DatabaseSetup(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if (modelName == "Student")
{
    var studentRepository = new StudentRepository(databaseConfig);
    if (modelAction == "New")
    {
        var registration = (args[2]);
        var name = (args[3]);
        var city = (args[4]);
        var formed = Convert.ToBoolean(args[5]); 

        var student = new Student (registration, name, city, formed);


        if (!studentRepository.ExistsById(registration))
        {
            studentRepository.Save(student);
            Console.WriteLine($"Estudante {name} cadastrado com sucesso");
        }
        else { Console.WriteLine($"Estudante com Id {registration} ja existe"); }
    }

    if (modelAction == "Delete")
    {
        var registration = (args[2]);

        if (studentRepository.ExistsById(registration))
        {
            studentRepository.Delete(registration);
            Console.WriteLine($"Estudante {registration} removido com sucesso");
        }
        else { Console.WriteLine($"Estudante com Resgistration {registration} nao encontrado"); }
    }

    if (modelAction == "MarkAsFormed")
    {
        var registration = (args[2]);

        if (studentRepository.ExistsById(registration))
        {
            studentRepository.MarkAsFormed(registration);
            Console.WriteLine($"Estudante {registration} definido como formado");
        }
        else { Console.WriteLine($"Estudante com Resgistration {registration} nao encontrado"); }
    }

    if (modelAction == "List")
    {
        var students = studentRepository.GetAll();

        if (students.Any())
        {
            foreach (var student in studentRepository.GetAll())
            {
                if (student.Former)
                {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");
                }
                else {Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, nao formado");}
            }
        }
        else { Console.WriteLine("Nenhum estudante cadastrado"); }
    }

    if (modelAction == "ListFormed")
    {
        var students = studentRepository.GetAllFormed();

        if (students.Any())
        {
            foreach (var student in studentRepository.GetAllFormed())
            {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");
            }
        }
        else { Console.WriteLine("Nenhum estudante cadastrado"); }
    }

    if (modelAction == "ListByCity")
    {
        var city = args[2];

        var students = studentRepository.GetAllStudentByCity(city);

        if (students.Any())
        {
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {student.Former}");
            }
        }
        else { Console.WriteLine("Nenhum estudante cadastrado"); }
    }

    if (modelAction == "ListByCities")
    {
        string[] cities = new string [args.Length - 2];

        for (int i = 2; i < args.Length; ++i)
        {
            cities[i - 2] = args[i];
        }

        var students = studentRepository.GetAllByCities(cities);

        if (students.Any())
        {
            foreach (var student in students)
            {
                if (student.Former)
                {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");
                }
                else { Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, nao formado"); }
            }
        }
        else { Console.WriteLine("Nenhum estudante cadastrado"); }
    }

    if (modelAction == "Report")
    {
        var modelActionTwo = args[2];

        if (modelActionTwo == "CountByCities")
        {
            var students = studentRepository.CountByCities();

            if (students.Any())
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.AttributeName}, {student.StudentNumber}");
                }
            } 
            else { Console.WriteLine("Nenhum estudante cadastrado"); }
        }

        if (modelActionTwo == "CountByFormed")
        {
            var students = studentRepository.CountByFormed();

            if (students.Any())
            {
                foreach (var student in students)
                {
                    if (student.AttributeName == "1")
                    {
                    Console.WriteLine($"formado, {student.StudentNumber}");
                    }
                    else { Console.WriteLine($"nao formado, {student.StudentNumber}"); }
                }
            }
            else { Console.WriteLine("Nenhum estudante cadastrado"); }
        }
    }
}