using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using QLSV.Data.Entities;
using QLSV.Data.Repositories.Contracts;
using QLSV.Protos;

namespace QLSV.Business.Services
{
    public class StudentService : StudentGRPC.StudentGRPCBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRepository _classRepository;
        public StudentService(IStudentRepository studentRepository, IClassRepository classRepository, ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
        }

        public override async Task<GetAllStudentsReply> GetAllStudents(GetAllStudentsRequest request, ServerCallContext context)
        {
            GetAllStudentsReply response = new GetAllStudentsReply();
            try
            {
                List<Student>? students = await _studentRepository.GetAllStudentsAsync();
                if (students == null || students.Count == 0)
                {
                    throw new Exception($"There is no student in database");
                }

                response.IsExists = true;
                foreach (var student in students)
                {
                    response.Students.Add(new StudentProfile
                    {
                        Id = student.Id,
                        FullName = student.FullName,
                        Birthday = student.Birthday.ToShortDateString(),
                        Address = student.Address,
                        ClassName = student.StudentClass.Name
                    });
                }
            }
            catch (Exception ex)
            {
                response.IsExists = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public override async Task<GetStudentDetailsByIdReply> GetStudentDetailsById(GetStudentDetailsByIdRequest request, ServerCallContext context)
        {
            GetStudentDetailsByIdReply response = new GetStudentDetailsByIdReply();
            try
            {
                Student? student = await _studentRepository.GetStudentDetailsByIdAsync(request.Id);
                if(student == null)
                {
                    throw new Exception($"There is no student_id = {request.Id}");
                }

                response.IsExists = true;
                response.StudentDetails = new StudentDetails
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Birthday = student.Birthday.ToShortDateString(),
                    Address = student.Address,
                    ClassId = student.StudentClass.Id,
                    ClassName = student.StudentClass.Name,
                    ClassSubject = student.StudentClass.Subject,
                    ClassTeacherId = student.StudentClass.ClassTeacher.Id,
                    ClassTeacherFullName = student.StudentClass.ClassTeacher.FullName,
                    ClassTeacherBirthday = student.StudentClass.ClassTeacher.Birthday.ToShortDateString()
                };
            }
            catch (Exception ex)
            {
                response.IsExists = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public override async Task<AddNewStudentReply> AddNewStudent(AddNewStudentRequest request, ServerCallContext context)
        {
            AddNewStudentReply response = new AddNewStudentReply();
            try
            {
                Class? studentClass = await _classRepository.GetClassByIdAsync(request.ClassId);
                if(studentClass == null)
                {
                    throw new Exception($"There is no class_id = {request.ClassId}");
                }
                var student = new Student
                {
                    FullName = request.FullName,
                    Birthday = DateTime.ParseExact(request.Birthday, "dd/MM/yyyy", null),
                    Address = request.Address,
                    StudentClass = studentClass,
                };
                await _studentRepository.AddNewStudentAsync(student);

                response.Success = true;
                response.Student = new StudentProfile
                {
                    FullName = student.FullName,
                    Birthday = student.Birthday.ToShortDateString(),
                    Address = student.Address,
                    ClassName = student.StudentClass.Name,
                };
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public override async Task<DeleteStudentReply> DeleteStudent(DeleteStudentRequest request, ServerCallContext context)
        {
            var response = new DeleteStudentReply();
            try
            {
                Student? student = await _studentRepository.GetStudentByIdAsync(request.Id);
                if (student == null)
                {
                    throw new Exception($"There is no student_id = {request.Id}");
                }

                await _studentRepository.DeleteStudentAsync(student);
                response.Success = true;
            }
            catch(Exception ex )
            {
                response.Success= false;
                response.Message = ex.Message;
            }
            return response;
        }

        public override async Task<UpdateStudentReply> UpdateStudent(UpdateStudentRequest request, ServerCallContext context)
        {
            var response = new UpdateStudentReply();
            try
            {
                Student? student = await _studentRepository.GetStudentByIdAsync(request.Id);
                if(student == null)
                {
                    throw new Exception($"There is no student_id = {request.Id}");
                }

                Class? studentClass = await _classRepository.GetClassByIdAsync(request.ClassId);
                if (studentClass == null)
                {
                    throw new Exception($"There is no class_id = {request.ClassId}");
                }

                student.FullName = request.FullName;
                student.Address = request.Address;
                student.Birthday = DateTime.ParseExact(request.Birthday, "dd/MM/yyyy", null);
                student.StudentClass = studentClass;
                await _studentRepository.UpdateStudentAsync(student);

                response.Success = true;
                response.Student.Id = student.Id;
                response.Student.FullName = student.FullName;
                response.Student.Birthday = student.Birthday.ToShortDateString();
                response.Student.Address = student.Address;
                response.Student.ClassName = student.StudentClass.Name;
            }
            catch (Exception ex)
            {
                response.Success= false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
