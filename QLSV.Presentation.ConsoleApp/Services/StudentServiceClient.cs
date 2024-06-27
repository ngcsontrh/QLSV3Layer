using Grpc.Core;
using Grpc.Net.Client;
using QLSV.Data.Entities;
using QLSV.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Presentation.ConsoleApp.Services
{
    public class StudentServiceClient
    {
        private readonly StudentGRPC.StudentGRPCClient _client;

        public StudentServiceClient(string address)
        {
            var channel = GrpcChannel.ForAddress(address);
            _client = new StudentGRPC.StudentGRPCClient(channel);
        }

        public void GetAllStudents()
        {
            try
            {
                var request = new GetAllStudentsRequest();
                var response = _client.GetAllStudents(request);

                if (response.IsExists)
                {
                    foreach (var student in response.Students)
                    {
                        Console.WriteLine($"Student Id: {student.Id}");
                        Console.WriteLine($"Full Name: {student.FullName}");
                        Console.WriteLine($"Birthday: {student.Birthday}");
                        Console.WriteLine($"Address: {student.Address}");
                        Console.WriteLine($"Class Name: {student.ClassName}");
                        Console.WriteLine("--------------");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.Message}");
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"RPC Exception: {ex.Message}");
            }
        }

        public void SortStudentsByName()
        {
            try
            {
                var request = new GetAllStudentsRequest();
                var response = _client.GetAllStudents(request);

                if (response.IsExists)
                {
                    var sortedStudent = response.Students.OrderBy(s => s.FullName);
                    
                    foreach (var student in sortedStudent)
                    {
                        Console.WriteLine($"Student Id: {student.Id}");
                        Console.WriteLine($"Full Name: {student.FullName}");
                        Console.WriteLine($"Birthday: {student.Birthday}");
                        Console.WriteLine($"Address: {student.Address}");
                        Console.WriteLine($"Class Name: {student.ClassName}");
                        Console.WriteLine("--------------");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.Message}");
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"RPC Exception: {ex.Message}");
            }
        }

        public void GetStudentDetailsById()
        {
            try
            {
                var request = new GetStudentDetailsByIdRequest();

                Console.Write("Nhap Id sinh vien can tim kiem: ");
                request.Id =  Convert.ToInt32(Console.ReadLine());

                var response = _client.GetStudentDetailsById(request);
                if(response.IsExists)
                {
                    StudentDetails student = response.StudentDetails;
                    Console.WriteLine($"Student Id: {student.Id}");
                    Console.WriteLine($"Full Name: {student.FullName}");
                    Console.WriteLine($"Birthday: {student.Birthday}");
                    Console.WriteLine($"Address: {student.Address}");
                                
                    Console.WriteLine($"Class Id: {student.ClassId}");
                    Console.WriteLine($"Class Name: {student.ClassName}");
                    Console.WriteLine($"Class Subject: {student.ClassSubject}");
                                
                    Console.WriteLine($"Teacher Id: {student.ClassTeacherId}");
                    Console.WriteLine($"Teacher Full Name: {student.ClassTeacherFullName}");
                    Console.WriteLine($"Teacher Birthday: {student.ClassTeacherBirthday}");                    
                }
                else
                {
                    Console.WriteLine(response.Message);
                }
            }
            catch(RpcException ex)
            {
                Console.WriteLine($"RPC Exception: {ex.Message}");
            }
        }

        public void AddNewStudent()
        {
            try
            {
                var request = new AddNewStudentRequest();

                Console.Write($"Nhap ten sinh vien");
                request.FullName = Console.ReadLine();

                Console.Write("Nhap ngay sinh sinh vien: ");
                request.Birthday = Console.ReadLine();

                Console.Write("Nhap dia chi hoc sinh: ");
                request.Address = Console.ReadLine();

                Console.Write("Nhap ma lop hoc: ");
                request.ClassId = Convert.ToInt32(Console.ReadLine());

                var response = _client.AddNewStudent(request);
                if (response.Success)
                {
                    var student = response.Student;
                    Console.WriteLine("Da them moi sinh vien co thong tin:");
                    Console.WriteLine($"Student Id: {student.Id}");
                    Console.WriteLine($"Full Name: {student.FullName}");
                    Console.WriteLine($"Birthday: {student.Birthday}");
                    Console.WriteLine($"Address: {student.Address}");
                    Console.WriteLine($"Class Name: {student.ClassName}");
                }
                else
                {
                    Console.WriteLine(response.Message);
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateStudent()
        {
            try
            {
                var request = new UpdateStudentRequest();

                Console.Write("Nhap MSV sinh vien can chinh sua: ");
                request.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Nhap ho ten: ");
                request.FullName= Console.ReadLine();

                Console.Write("Nhap ngay sinh hoc sinh: ");
                request.Birthday = Console.ReadLine();

                Console.Write("Nhap dia chi hoc sinh: ");
                request.Address = Console.ReadLine();

                Console.Write("Nhap ma lop hoc: ");
                request.ClassId= Convert.ToInt32(Console.ReadLine());

                var response = _client.UpdateStudent(request);
                if (response.Success)
                {
                    var student = response.Student;
                    Console.WriteLine("Da cap nhat thong sin sinh vien");
                    Console.WriteLine("Da cap nhat sinh vien co thong tin:");
                    Console.WriteLine($"Student Id: {student.Id}");
                    Console.WriteLine($"Full Name: {student.FullName}");
                    Console.WriteLine($"Birthday: {student.Birthday}");
                    Console.WriteLine($"Address: {student.Address}");
                    Console.WriteLine($"Class Id: {student.ClassId}");
                    Console.WriteLine($"Class Name: {student.ClassName}");
                }
                else
                {
                    Console.WriteLine(response.Message);
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteStudent()
        {
            try
            {
                var request = new DeleteStudentRequest();
                
                Console.Write("Nhap MSV sinh vien can xoa: ");
                request.Id = Convert.ToInt32(Console.ReadLine());

                var response = _client.DeleteStudent(request);
                if (response.Success)
                {
                    Console.WriteLine($"Da xoa sinh vien co MSV {request.Id}");
                }
                else
                {
                    Console.WriteLine(response.Message);
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
