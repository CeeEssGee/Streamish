import Video from "../components/Video";
import { getToken } from "./authManager";

// the proxy in package.json is prepended, so the forward slash at the beginning of the baseUrl is very important 
const baseUrl = '/api/video';
const getWithCommentsUrl = baseUrl + "/GetWithComments"

export const getAllVideos = () => {
    return getToken().then((token) => {
        return fetch(getWithCommentsUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get quotes.",
                );
            }
        });
    });
};

export const addVideo = (video) => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify(video),
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else if (resp.status === 401) {
                throw new Error("Unauthorized");
            } else {
                throw new Error(
                    "An unknown error occurred while trying to save a new video.",
                );
            }
        });
    });
};


export const searchVideos = (searchTerm) => {
    const searchUrl = baseUrl + "/search?q=" + searchTerm + "&sortDesc=true"

    return getToken().then((token) => {
        return fetch(searchUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get videos.",
                );
            }
        });
    });

};

export const getVideo = (id) => {
    const videoUrl = `${getWithCommentsUrl}/${id}`

    return getToken().then((token) => {
        return fetch(videoUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get videos.",
                );
            }
        });
    });

};

export const getUserVideos = (id) => {
    const userVideosUrl = `/api/userProfile/GetByIdWithVideos/${id}`

    return getToken().then((token) => {
        return fetch(userVideosUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to save a new video.",
                );
            }
        });
    });
}