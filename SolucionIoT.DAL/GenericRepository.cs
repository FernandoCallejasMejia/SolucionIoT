using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using LiteDB;
using FluentValidation;
using System.Linq;

namespace SolucionIoT.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDTO
    {
#if DEBUG
        readonly string dbName = @"DBIoT.db";//debug o local
#else
        readonly string dbName = @"d:\DZHosts\LocalUser\EquipoKobra\Protected.solucionIoTKobra.somee.com\DBIoT.db";//somme.com

#endif
        public string Error { get;private set; }

        readonly AbstractValidator<T> validator;

        public IEnumerable<T> Read 
        {
            get
            {
                try
                {
                    IEnumerable<T> entidades;
                    using (var db = new LiteDatabase(new ConnectionString() { Filename = dbName }))
                    {
                        entidades = db.GetCollection<T>(typeof(T).Name).FindAll().ToList();
                    }
                    Error = entidades != null ? "" : "No se encontraron la entidad";
                    return entidades;
                }
                catch (Exception ex)
                {

                    Error = ex.Message;
                    return null;
                }
            }
        }

        public GenericRepository(AbstractValidator<T> validator)
        {
            this.validator = validator;
        }

        public T Create(T entidad)
        {
            try
            {
                entidad.Id = Guid.NewGuid().ToString();
                entidad.FechaHora = DateTime.Now;
               
                var resultV = validator.Validate(entidad);
                if (resultV.IsValid)
                {
                    using (var db = new LiteDatabase(new ConnectionString() { Filename = dbName }))
                    {
                        db.GetCollection<T>(typeof(T).Name).Insert(entidad);
                    }
                    Error = "";
                    return entidad;
                }
                else
                {
                    Error = "Errores de validacion:";
                    foreach (var item in resultV.Errors)
                    {
                        Error += item.ErrorMessage + ";";

                    }
                    return null;
                }
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return null;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                using (var db=new LiteDatabase(new ConnectionString() {Filename=dbName }))
                {
                    if (db.GetCollection<T>(typeof(T).Name).Delete(id)) 
                    {
                        Error = "";
                        return true;
                    }
                    else
                    {
                        Error = "No se pudo eliminar la entidad";
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
       
            }       
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicado)
        {
            try
            {
               IEnumerable<T>  entidades;
                using (var db = new LiteDatabase(new ConnectionString() { Filename = dbName }))
                {
                    entidades = db.GetCollection<T>(typeof(T).Name).Find(predicado).ToList();
                }
                Error = entidades != null ? "" : "No se encontraron la entidad";
                return entidades;
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return null;
            }
        }

        public T SearchById(string id)
        {
            try
            {
                T entidad;
                using (var db=new LiteDatabase(new ConnectionString() {Filename= dbName }))
                {
                    entidad = db.GetCollection<T>(typeof(T).Name).FindById(id);
                }
                Error = entidad != null ? "" : "No se encontro la entidad";
                return entidad;
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return null;
            }
        }

        public T Update(T entidad)
        {
            try
            {
           
                var resultV = validator.Validate(entidad);
                if (resultV.IsValid)
                {
                    bool r;
                    using (var db = new LiteDatabase(new ConnectionString() { Filename = dbName }))
                    {
                       r= db.GetCollection<T>(typeof(T).Name).Update(entidad);
                    }
                    if (r)
                    {
                        Error = "";
                        return entidad;
                    }
                    else
                    {
                        Error = "No se actualizo la entidad";
                        return null;

                    }
              
                }
                else
                {
                    Error = "Errores de validacion:";
                    foreach (var item in resultV.Errors)
                    {
                        Error += item.ErrorMessage + ";";

                    }
                    return null;
                }
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return null;
            }
        }
    }
}
