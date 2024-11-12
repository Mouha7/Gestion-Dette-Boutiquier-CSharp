using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Main.Core.Repository.Implement;

public class RepositoryDB<T> : IRepositoryDB<T>
    {
        private string connectionString = "server=localhost;database=gestion_dette_csharp_web;user=root;password=root";
        
        // Création de la connexion MySQL
        private IDbConnection dbConnection => new MySqlConnection(connectionString);

        // Méthode pour récupérer tous les enregistrements
        IEnumerable<T> IRepositoryDB<T>.selectAll()
        {
            using (var dbConnection = this.dbConnection)
            {
                dbConnection.Open();
                string query = $"SELECT * FROM {typeof(T).Name}";
                return dbConnection.Query<T>(query);
            }
        }

        // Méthode pour récupérer un enregistrement par son Id
        T? IRepositoryDB<T>.selectBy(int id)
        {
            using (var dbConnection = this.dbConnection)
            {
                dbConnection.Open();
                string query = $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id";
                return dbConnection.QuerySingleOrDefault<T>(query, new { Id = id });
            }
        }

        // Méthode pour insérer un enregistrement
        int IRepositoryDB<T>.insert(T entity)
        {
            using (var dbConnection = this.dbConnection)
            {
                dbConnection.Open();
                string query = GenerateInsertQuery();
                return dbConnection.Execute(query, entity);
            }
        }

        // Méthode pour mettre à jour un enregistrement
        int IRepositoryDB<T>.Update(T entity)
        {
            using (var dbConnection = this.dbConnection)
            {
                dbConnection.Open();
                string query = GenerateUpdateQuery();
                return dbConnection.Execute(query, entity);
            }
        }

        // Méthode pour supprimer un enregistrement par Id
        int IRepositoryDB<T>.Delete(int id)
        {
            using (var dbConnection = this.dbConnection)
            {
                dbConnection.Open();
                string query = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";
                return dbConnection.Execute(query, new { Id = id });
            }
        }

        // Génère dynamiquement une requête d'insertion
        private string GenerateInsertQuery()
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var paramNames = string.Join(", ", properties.Select(p => "@" + p.Name));

            return $"INSERT INTO {typeof(T).Name} ({columnNames}) VALUES ({paramNames})";
        }

        // Génère dynamiquement une requête de mise à jour
        private string GenerateUpdateQuery()
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            return $"UPDATE {typeof(T).Name} SET {setClause} WHERE Id = @Id";
        }
    }