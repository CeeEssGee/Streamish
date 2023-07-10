SELECT * FROM Video;
SELECT * FROM Favorite;
SELECT * FROM Comment;
SELECT * FROM UserProfile;

--Get all videos
SELECT v.Id, v.Title, v.Description, v.Url, v.DateCreated, v.UserProfileId,
                      up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
                      up.ImageUrl AS UserProfileImageUrl
                 FROM Video v 
                      JOIN UserProfile up ON v.UserProfileId = up.Id
             ORDER BY DateCreated


--Get all videos with comments
SELECT v.Id AS VideoId, v.Title, v.Description, v.Url, 
                       v.DateCreated AS VideoDateCreated, v.UserProfileId As VideoUserProfileId,
                     up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
                       up.ImageUrl AS UserProfileImageUrl,
                       c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
                  FROM Video v 
                       JOIN UserProfile up ON v.UserProfileId = up.Id
                       LEFT JOIN Comment c on c.VideoId = v.id
             ORDER BY  v.DateCreated


SELECT v.Title, v.Description, v.Url, v.DateCreated, v.UserProfileId, up.Name, up.Email, up.ImageUrl, up.DateCreated
                            FROM Video v
                            JOIN UserProfile up ON v.UserProfileId = up.Id
                           WHERE v.Id = 1


SELECT v.Id AS VideoId, v.Title, v.Description, v.Url, 
                       v.DateCreated AS VideoDateCreated, v.UserProfileId As VideoUserProfileId,

                       up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
                       up.ImageUrl AS UserProfileImageUrl,
                        
                       c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
                  FROM Video v 
                       JOIN UserProfile up ON v.UserProfileId = up.Id
                       LEFT JOIN Comment c on c.VideoId = v.id
                WHERE v.Id = 1
             ORDER BY  v.DateCreated

SELECT v.Id, v.Title, v.Description, v.Url, v.DateCreated, v.UserProfileId,

                       up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
                       up.ImageUrl AS UserProfileImageUrl,
                        
                       c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId

                    FROM Video v 
                       JOIN UserProfile up ON v.UserProfileId = up.Id
                       LEFT JOIN Comment c on c.VideoId = v.id
                    WHERE v.Id = 1
                     ORDER BY  v.DateCreated

SELECT up.Id, up.Name, up.Email, up.ImageUrl, up.DateCreated,

                            v.Id, v.Title, v.Description, v.Url, v.DateCreated
                            
                            FROM UserProfile up
                            JOIN Video v ON up.Id = v.UserProfileId
                            WHERE up.Id = 2
                            ORDER BY v.DateCreated