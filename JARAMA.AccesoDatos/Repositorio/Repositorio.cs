﻿using JAMARA.Data;
            return await dbSet.FindAsync(id); //select * from (solo por id)
            IQueryable<T> query = dbSet;
            {
                query = query.Where(filtro); //select * from where filtro
            }
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //ejemplo "Categoria,Marca"
                }
            }
            {
                query = query.AsNoTracking();
            }
            {
                query = query.Where(filtro); //select * from where filtro
            }
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //ejemplo "Categoria,Marca"
                }
            }
            {
                query = orderBy(query);
            }
            {
                query = query.AsNoTracking();
            }