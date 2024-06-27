using QLSV.Presentation.ConsoleApp.Services;

namespace QLSV.Presentation.ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string serverAddress = "http://localhost:5179"; // Replace with your server address
            var client = new StudentServiceClient(serverAddress);

            while (true)
            {
                Console.WriteLine("--------------------Quan ly sinh vien--------------------");
                Console.WriteLine("1. Xem danh sach sinh vien");
                Console.WriteLine("2. Them sinh vien");
                Console.WriteLine("3. Sua thong tin sinh vien");
                Console.WriteLine("4. Xoa sinh vien");
                Console.WriteLine("5. Sap xep sinh vien theo ten");
                Console.WriteLine("6. Tim kiem sinh vien theo MSV");
                Console.WriteLine("7. Thoat ung dung");
                Console.WriteLine("------------------------");
                Console.Write("Nhap lua chon: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        client.GetAllStudents();
                        break;
                    case 2:
                        client.AddNewStudent();
                        break;
                    case 3:
                        client.UpdateStudent();
                        break;
                    case 4:
                        client.DeleteStudent();
                        break;
                    case 5:
                        client.SortStudentsByName();
                        break;
                    case 6:
                        client.GetStudentDetailsById();
                        break;
                    case 7:
                        return;
                }
            }
        }
    }
}
