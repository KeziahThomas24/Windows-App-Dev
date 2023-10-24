using Library_management.Middleware;
using AutoMapper;
using Library_management.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Library_management.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Data.SqlClient;
using Castle.Core.Resource;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Connection = Library_management.Models.Connection;
using System.Windows.Forms;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net;

namespace Library_management.Middleware
{
    public class LibraryManagementDB
    {
        private LibraryManagementContext contextInstance = null;
        private readonly IMapper mapper;

        public LibraryManagementDB()
        {
            mapper = MapperConfig.InitializeAutomapper();
        }

        public LibraryManagementDB(IMapper _mapper)
        {
            mapper = _mapper;
        }

        //public MMABooksContext contextInstance
        //{
        //    get
        //    {
        //        if (context == null)
        //            context = new MMABooksContext();
        //        return context;
        //    }

        //}

       
                 public List<BooksDTO> GetAllBooks(string bookName)
        {

            contextInstance = new LibraryManagementContext();
            var books = contextInstance.Books.Where(x => x.BookName.Contains(bookName)).ToList();
            var allBooks = mapper.Map<List<BooksDTO>>(books);
            return allBooks;
        }
        public UsersDTO ChekingUser( UsersDTO users)
        {
            contextInstance = new LibraryManagementContext();
            var user = contextInstance.Users.Where(i => i.Email == users.Email && i.Password == users.Password).FirstOrDefault();
            var newUser = mapper.Map<UsersDTO>(user);

            return newUser;
        }

        /* public List<CustomerDTO> GetCustomers()
         {
             contextInstance = new MMABooksContext();
             var customer = contextInstance.Customers.ToList();
             var newCustomer = mapper.Map<List<CustomerDTO>>(customer);

             return newCustomer;
         }*/

        public List<CategoryDTO> GetCategory()
        {
            contextInstance = new LibraryManagementContext();
            var categories = contextInstance.Category.ToList();
            var allCategory = mapper.Map<List<CategoryDTO>>(categories);

            return allCategory;
        }

        /*   public int Remove(int customer, out string errorMessage)
           {
               errorMessage = string.Empty;
               var response = 0;
               try
               {
                   var newCustomer = mapper.Map<Customers>(customer);
                   contextInstance = new MMABooksContext();
                   contextInstance.Customers.Remove(newCustomer);
                   response = contextInstance.SaveChanges(true);
               }
               catch (DbUpdateException ex)
               {

                   var sqlException = (SqlException)
                       ex.InnerException;

                   foreach (SqlError error in sqlException.Errors)
                   {
                       errorMessage += "ERROR CODE: " + error.Number + " " + error.Message + "\n";
                   }
               }
               catch (Exception ex)
               {
                   errorMessage += $"{ex.Message}, {ex.GetType().ToString()}";
               }


               return response;

           }*/

        /*   public int UpdateCustomer(CustomerDTO customer, out string errorMessage)
           {
               errorMessage = string.Empty;
               var response = 0;
               Customers newCustomer = null;

               try
               {
                   newCustomer = mapper.Map<Customers>(customer);
                   contextInstance = new MMABooksContext();
                   contextInstance.Customers.Update(newCustomer);
                   response = contextInstance.SaveChanges(true);
               }

               catch (DbUpdateConcurrencyException ex)
               {

                   ex.Entries.Single().Reload();
                   if (contextInstance.Entry(newCustomer).State == EntityState.Detached)
                   {
                       errorMessage += "Another user has deleted that customer - Concurrency Error" + "\n";

                   }
                   else
                   {
                       errorMessage += "Another user has updated that customer.\n" + "The current database values will be displayed.";
                   }
               }
               catch (DbUpdateException ex)
               {
                   //
                   var sqlException = (SqlException)
                       ex.InnerException;

                   foreach (SqlError error in sqlException.Errors)
                   {
                       errorMessage += "ERROR CODE: " + error.Number + " " + error.Message + "\n";
                   }
               }
               catch (Exception ex)
               {
                   errorMessage += $"{ex.Message}, {ex.GetType().ToString()}";
               }
               return response;

           }
   */
        public void UpdateBook(int bookId,string  author, string description, int category)
        {

            var recordsToUpdate = contextInstance.Books.Where(x => x.BookId == bookId).FirstOrDefault();


            recordsToUpdate.AuthorName = author;
            recordsToUpdate.Description = description;
            recordsToUpdate.CategoryId = category;
            
            contextInstance.SaveChanges();
        }

        public int RegisterUser(UsersDTO users, out string errorMessage)
        {
            errorMessage = string.Empty;
            var response = 0;
            Users newUser = null;

            try
            {
                newUser = mapper.Map<Users>(users);
                contextInstance = new LibraryManagementContext();
                contextInstance.Users.Add(newUser);
                response = contextInstance.SaveChanges(true);
            }

            catch (DbUpdateConcurrencyException ex)
            {

                ex.Entries.Single().Reload();
                if (contextInstance.Entry(newUser).State == EntityState.Detached)
                {
                    errorMessage += "Another user has deleted that customer - Concurrency Error" + "\n";

                }
                else
                {
                    errorMessage += "Another user has updated that customer.\n" + "The current database values will be displayed.";
                }
            }
            catch (DbUpdateException ex)
            {
                //
                var sqlException = (SqlException)
                    ex.InnerException;

                foreach (SqlError error in sqlException.Errors)
                {
                    errorMessage += "ERROR CODE: " + error.Number + " " + error.Message + "\n";
                }
            }
            catch (Exception ex)
            {
                errorMessage += $"{ex.Message}, {ex.GetType().ToString()}";
            }
            return response;

        }

        public int AddBook(BooksDTO books, out string errorMessage)
        {
            errorMessage = string.Empty;
            var response = 0;
            Books newBook = null;

            try
            {
                newBook = mapper.Map<Books>(books);
                contextInstance = new LibraryManagementContext();
                contextInstance.Books.Add(newBook);
                response = contextInstance.SaveChanges(true);
            }

            catch (DbUpdateConcurrencyException ex)
            {

                ex.Entries.Single().Reload();
                if (contextInstance.Entry(newBook).State == EntityState.Detached)
                {
                    errorMessage += "Another user has deleted that customer - Concurrency Error" + "\n";

                }
                else
                {
                    errorMessage += "Another user has updated that customer.\n" + "The current database values will be displayed.";
                }
            }
            catch (DbUpdateException ex)
            {
                //
                var sqlException = (SqlException)
                    ex.InnerException;

                foreach (SqlError error in sqlException.Errors)
                {
                    errorMessage += "ERROR CODE: " + error.Number + " " + error.Message + "\n";
                }
            }
            catch (Exception ex)
            {
                errorMessage += $"{ex.Message}, {ex.GetType().ToString()}";
            }
            return response;

        }
        
             public List<BooksDTO> GetAllBooksAvailable()
          {

                contextInstance = new LibraryManagementContext();
                var books = contextInstance.Books.Where(x => x.ReservedBy == null && x.LendedBy == null).ToList();
                var allBooks = mapper.Map<List<BooksDTO>>(books);

            return allBooks;
        }

       
        public BooksDTO GetBookDetails(int Id)
        {
            contextInstance = new LibraryManagementContext();
            var book = contextInstance.Books.Include(b => b.Category).Where(i => i.BookId == Id).FirstOrDefault();
            var newbook = mapper.Map<BooksDTO>(book);
            return newbook;
        }

     
             public void LendBook(int UserId, int[] ids)
        {

            var recordsToUpdate = contextInstance.Books.Where(x => ids.Contains(x.BookId)).ToList();

            foreach (var record in recordsToUpdate)
            {
                record.LendedBy = UserId;
            }
            contextInstance.SaveChanges();      

        }

        public int getLendCount( int Id)
        {
            contextInstance = new LibraryManagementContext();
            var result = contextInstance.Books.Where(x => x.LendedBy == Id).ToList();
            return result.Count;
        }

        public List<BooksDTO> GetAllLendedBooks(int id)
        {

            contextInstance = new LibraryManagementContext();
            var books = contextInstance.Books.Where(x => x.LendedBy == id).ToList();
            var allBooks = mapper.Map<List<BooksDTO>>(books);

            return allBooks;
        }
       

        public void ReturnBook(int UserId, int[] ids)
        {

            var recordsToUpdate = contextInstance.Books.Where(x => ids.Contains(x.BookId)).ToList();

            foreach (var record in recordsToUpdate)
            {
                record.LendedBy = record.ReservedBy;
                record.ReservedBy = null;
            }
            contextInstance.SaveChanges();

        }

        public List<BooksDTO> GetAllBookstoReserve(int userId)
        {
            contextInstance = new LibraryManagementContext();
            var books = contextInstance.Books.Where(x => x.ReservedBy == null && x.LendedBy != null && x.LendedBy != userId).ToList();
            var allBooks = mapper.Map<List<BooksDTO>>(books);
            return allBooks;
        }

        public void ReserveBook(int UserId, int id)
        {

            var recordToUpdate = contextInstance.Books.Where(x => x.BookId == id).FirstOrDefault();
            recordToUpdate.ReservedBy = UserId;       
            contextInstance.SaveChanges();

        }

        public BooksDTO ReservedBook(int Id)
        {
            contextInstance = new LibraryManagementContext();
            var result = contextInstance.Books.Include(b => b.Category).Where(x => x.ReservedBy == Id).FirstOrDefault();
            var newresult = mapper.Map<BooksDTO>(result);
            /*var result = contextInstance.Books.Include(b => b.Category).Where(i => i.BookId == Id).FirstOrDefault();
            var newresult = mapper.Map<BooksDTO>(result);*/
            return newresult;
        }


    }
}
