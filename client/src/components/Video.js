import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

const Video = ({ video, userName }) => {
    return (
        <Card >

            <Link to={`/users/${video.userProfileId}`} >
                Posted by: {userName}
            </Link>


            <CardBody>
                <iframe className="video"
                    src={video.url}
                    title="YouTube video player"
                    frameBorder="0"
                    allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                    allowFullScreen />

                <Link to={`/videos/${video.id}`}>
                    <p>
                        <strong>{video.title}</strong>
                    </p>
                </Link>
                <p>{video.description}</p>
                {/* <h3>Comments</h3>
                <ul>
                    {video.comments?.map(c =>
                    (
                        <li>{c.message}</li>
                    ))}
                </ul> */}
            </CardBody>
        </Card>
    );
};

export default Video;