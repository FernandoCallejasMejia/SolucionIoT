using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Driver;
using FluentValidation;
using FluentValidation.Results;

namespace SolucionIoT.DAL
{
    public class GenericRepositoryMongoDB<T> : IGenericRepository<T> where T : BaseDTO
    {
        MongoClient client;
        IMongoDatabase db;
        AbstractValidator<T> validator;
        ValidationResult resultV;

        public GenericRepositoryMongoDB(AbstractValidator<T> validator)
        {
            this.validator = validator;
            var client = new MongoClient("mongodb+srv://TeamKobra:123Kobra@solucioniotkobra.6yh24.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            db = client.GetDatabase("SolucionIoTKobra");
        }

        private IMongoCollection<T> Collection() => db.GetCollection<T>(typeof(T).Name);

        public string Error { get; private set; }

        public IEnumerable<T> Read => Collection().AsQueryable();

        public T Create(T entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            entidad.FechaHora = DateTime.Now;
            resultV = validator.Validate(entidad);
            if (resultV.IsValid)
            {
                try
                {
                    Collection().InsertOne(entidad);
                    Error = "";
                    return entidad;
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    return null;
                }
            }
            else
            {
                Error = $"Datos inválidos: ";
                foreach (var error in resultV.Errors)
                {
                    Error += "\n" + error.ErrorMessage;
                }
                return null;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                int r = (int)Collection().DeleteOne(e => e.Id == id).DeletedCount;
                Error = r == 1 ? "" : $"Se eliminaron {r} elementos";
                return r == 1;
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
                return Collection().Find(predicado).ToEnumerable();
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
                return Collection().Find(e => e.Id == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        public T Update(T entidad)
        {
            resultV = validator.Validate(entidad);
            if (resultV.IsValid)
            {
                try
                {
                    int r = (int)Collection().ReplaceOne(e => e.Id == entidad.Id, entidad).ModifiedCount;
                    Error = "";
                    return r == 1 ? entidad : null;
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    return null;
                }

            }
            else
            {
                Error = $"Datos inválidos: ";
                foreach (var error in resultV.Errors)
                {
                    Error += "\n" + error.ErrorMessage;
                }
                return null;
            }
        }
    }
}
