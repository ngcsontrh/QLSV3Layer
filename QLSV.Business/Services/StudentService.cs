using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using QLSV.Business.Protos;
using QLSV.Data.Entities;
using QLSV.Data.Repositories.Contracts;
using QLSV.DTO.StudentDTO;

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

        public override async Task<GetAllStudentReply> GetAllStudent(GetAllStudentRequest request, ServerCallContext context)
        {
            GetAllStudentReply response = new GetAllStudentReply();
            try
            {
                List<GetAllStudentDTO>? students = await _studentRepository.GetAllStudentsAsync();

                if(students == null || students.Count == 0)
                {
                    throw new Exception($"There is no student in database");
                }

                response.IsExist = true;
                foreach (var student in students)
                {
                    response.Students.Add(new GetStudentReply
                    {
                        Id = student.Id,
                        FullName = student.FullName,
                        Birthday = student.Birthday,
                        Address = student.Address,
                        ClassName = student.ClassName
                    });
                }
            }
            catch (Exception ex)
            {
                response.IsExist= false;
                response.Message = ex.Message;
            }
            return response;
        }

        public override async Task<GetDetailStudentReply> GetDetailStudentById(GetDetailStudentByIdRequest request, ServerCallContext context)
        {
            GetDetailStudentReply response = new GetDetailStudentReply();
            try
            {
                GetDetailStudentByIdDTO? student = await _studentRepository.GetDetailStudentByIdAsync(request.Id);

                if(student == null)
                {
                    throw new Exception($"There is no student_id = {request.Id}");
                }

                response.IsExists = true;
                response.Id = student.Id;
                response.FullName = student.FullName;
                response.Birthday = student.Birthday;
                response.Address = student.Address;
                response.ClassName = student.ClassName;
                response.ClassSubject = student.ClassSubject;
                response.ClassTeacherFullName = student.ClassTeacherFullName;
                response.ClassTeacherBirthday = student.ClassTeacherBirthday;
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
                    StudentClass = studentClass
                };
                await _studentRepository.AddNewStudentAsync(student);
                response.Success = true;
                response.StudentInfo = request;
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
                response.StudentInfo = request;
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
