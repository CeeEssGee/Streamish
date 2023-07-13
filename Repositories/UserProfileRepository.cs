using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Streamish.Models;
using Streamish.Utils;

namespace Streamish.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, Up.FirebaseUserId, up.Name AS UserProfileName, up.Email, up.ImageUrl, up.DateCreated
                          FROM UserProfile up                               
                         WHERE FirebaseUserId = @FirebaseuserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Name = DbUtils.GetString(reader, "UserProfileName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            // ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }


        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Name, Email, ImageUrl, DateCreated
                        FROM UserProfile
                        ORDER BY Name
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            userProfiles.Add(new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                            });

                        }

                        return userProfiles;

                    }
                }
            }
        }


        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT Id, Name, Email, ImageUrl, DateCreated
                            FROM UserProfile
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        UserProfile userProfile = null;
                        if (reader.Read())
                        {
                            userProfile = new UserProfile()
                            {
                                Id = id,
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            };
                        }

                        return userProfile;
                    }
                }
            }
        }

        public UserProfile GetByIdWithVideos(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, up.Name, up.Email, up.ImageUrl, up.DateCreated,

                            v.Id AS VideoId, v.Title, v.Description, v.Url, v.DateCreated AS VideoDateCreated
                            
                            FROM UserProfile up
                            JOIN Video v ON up.Id = v.UserProfileId
                            WHERE up.Id = @id
                            ORDER BY v.DateCreated
                        ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile userProfile = null;
                        while (reader.Read())
                        {
                            
                            if (userProfile == null)
                            {
                                userProfile = new UserProfile()
                                {
                                    Id = id,
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                    DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                    Videos = new List<Video>()
                                };

                            }
                            if (DbUtils.IsNotDbNull(reader, "VideoId"))
                            {
                                userProfile.Videos.Add(new Video()
                                {
                                    Id = DbUtils.GetInt(reader, "VideoId"),
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                    Url = DbUtils.GetString(reader, "Url"),
                                    DateCreated = DbUtils.GetDateTime(reader, "VideoDateCreated")
                                });
                            }
                        }
                        return userProfile;
                    }
                }
            }
        }

        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile (Name, Email, ImageUrl, DateCreated)
                        OUTPUT INSERTED.ID
                        VALUES (@Name, @Email, @ImageUrl, @DateCreated)";

                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userProfile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@DateCreated", userProfile.DateCreated);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                            SET Name = @Name,
                                Email = @Email,
                                ImageUrl = @ImageUrl,
                                DateCreated = @DateCreated
                            WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userProfile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@DateCreated", userProfile.DateCreated);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE 
                            FROM UserProfile 
                            WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();

                }
            }
        }


    }
}
