import React, { useEffect, useState } from "react";
import { ListGroup, ListGroupItem } from "reactstrap";
import { useParams } from "react-router-dom";
import { getUserVideos } from "../modules/videoManager";
import Video from "./Video";

const UserVideos = () => {
    const [user, setUser] = useState();
    const { id } = useParams();

    useEffect(() => {
        getUserVideos(id).then(setUser);
    }, []);

    if (!user) {
        return null;
    }

    return (
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-sm-12 col-lg-6">
                    {
                        user.videos.length > 0 ?
                            user.videos?.map((video) => (
                                <Video video={video} key={video.id} userName={user.name}/>
                            ))
                            : <h2>No Result Found</h2>
                    }
                </div>
            </div>
        </div>
    );
};

export default UserVideos;