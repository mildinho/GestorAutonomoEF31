using GestorAutonomo.Data;
using GestorAutonomo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Services
{
    public class LoginService
    {
        private readonly GestorAutonomoContext _context;

        public LoginService(GestorAutonomoContext context)
        {
            _context = context;
        }


        public void InsertAsync(Login login)
        {
            _context.Login.Add(login);
            _context.SaveChangesAsync();
        }


        public void RemoveAsync(int Id)
        {
            var obj = _context.Login.Find(Id);

            if (obj != null)
            {

                _context.Login.Remove(obj);
                _context.SaveChanges();
            }


        }

        public void UpdateAsync(Login obj)
        {
            _context.Login.Update(obj);
            _context.SaveChanges();

        }





        public async Task<Login> FindByIDAsync(int Id)
        {
            return await _context.Login.FirstOrDefaultAsync(obj => obj.Id == Id);
        }


        public Login Pesquisar(string Email, string Senha)
        {
            Login obj = _context.Login.Where(m => m.EMail == Email && m.Password == Senha).FirstOrDefault();
            return obj;
        }


    }
}
