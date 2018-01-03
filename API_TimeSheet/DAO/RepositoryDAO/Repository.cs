using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using API_TimeSheet.DAO.Data.ConfigData;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace API_TimeSheet.DAO.RepositoryDAO
{
    public class Repository<T> where T : class
    {
        MongoCollection collection;
        public Repository(string nameTable)
        {
            collection = new ConectionData().AbriConection().GetCollection(nameTable);
        }

        public IEnumerable<T> Listar()
        {
            return collection.FindAllAs<T>().ToList();
        }

        public IEnumerable<T> Listar(Expression<Func<T, bool>> where)
        {
            IQueryable<T> consulta = collection.AsQueryable<T>();

            if (where != null)
            { consulta = consulta.Where(where); }

            return consulta.ToList();
        }

        public T Consultar(Expression<Func<T, bool>> where)
        {
            IQueryable<T> consulta = collection.AsQueryable<T>();

            if (where != null)
            { consulta = consulta.Where(where); }

            return consulta.FirstOrDefault();
        }

        public void Inserir(T obj)
        {

            collection.Save(obj);
        }

        public void Atualizar(T obj)
        {
            collection.Save(obj);
        }

        public void Excluir(IMongoQuery query)
        {
            try
            {
                if (query != null)
                {
                    collection.Remove(query);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}