﻿using JAMARA.Data;using JARAMA.AccesoDatos.Repositorio.IRepositorio;using Microsoft.EntityFrameworkCore;using System;using System.Collections.Generic;using System.Linq;using System.Linq.Expressions;using System.Text;using System.Threading.Tasks;namespace JARAMA.AccesoDatos.Repositorio{    public class Repositorio<T> : IRepositorio<T> where T : class    {        private readonly ApplicationDbContext _db;        internal DbSet<T> dbSet;        public Repositorio(ApplicationDbContext db)        {            _db = db;            this.dbSet = _db.Set<T>();        }        public async Task Agregar(T entidad)        {            await dbSet.AddAsync(entidad); //insert into table        }        public async Task<T> Obtener(int id)        {
            return await dbSet.FindAsync(id); //select * from (solo por id)        }        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)        {
            IQueryable<T> query = dbSet;            if (filtro != null)
            {
                query = query.Where(filtro); //select * from where filtro
            }            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //ejemplo "Categoria,Marca"
                }
            }                       if (!isTracking)
            {
                query = query.AsNoTracking();
            }            return await query.FirstOrDefaultAsync();        }        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)        {            IQueryable<T> query = dbSet;            if(filtro != null)
            {
                query = query.Where(filtro); //select * from where filtro
            }            if(incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //ejemplo "Categoria,Marca"
                }
            }            if(orderBy != null)
            {
                query = orderBy(query);
            }            if(!isTracking)
            {
                query = query.AsNoTracking();
            }            return await query.ToListAsync();        }        public void Remover(T entidad)        {            dbSet.Remove(entidad);        }        public void RemoverRango(IEnumerable<T> entidad)        {            dbSet.RemoveRange(entidad);        }    }}