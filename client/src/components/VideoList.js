import React, { useEffect, useState } from "react";
import Video from './Video';
import { getAllVideos, searchVideos } from "../modules/videoManager";
import VideoForm from "./VideoForm";

const VideoList = () => {
    const [videos, setVideos] = useState([]);

    const getVideos = () => {
        getAllVideos().then(videos => setVideos(videos));
    };

    const searchVids = (event) => {
        let searchTerm = event.target.value;

        searchVideos(searchTerm).then(video => setVideos(video));
    }

    useEffect(() => {
        getVideos();
    }, []);

    return (
        <div className="container">
            {/* <VideoForm getVideos={getVideos} /> */}
            <div className="row justify-content-center">
                <input type="" placeholder="Search Videos" onChange={searchVids} />
                {
                    videos.length > 0 ?
                        videos?.map((video) => (
                            <Video video={video} key={video.id} />
                        ))
                        : <h2>No Result Found</h2>
                }
            </div>
        </div>
    );
};

export default VideoList;