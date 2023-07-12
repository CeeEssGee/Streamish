// the proxy in package.json is prepended, so the forward slash at the beginning of the baseUrl is very important 
const baseUrl = '/api/video';
const getWithCommentsUrl = baseUrl + "/GetWithComments"

export const getAllVideos = () => {
    return fetch(getWithCommentsUrl)
        .then((res) => res.json())
};

export const addVideo = (video) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(video),
    });
};

export const searchVideos = (searchTerm) => {
    return fetch(baseUrl + "/search?q=" + searchTerm + "&sortDesc=true")
        .then((res) => res.json());
};

export const getVideo = (id) => {
    return fetch(`${getWithCommentsUrl}/${id}`)
        .then((res) => res.json());
};

export const getUserVideos = (id) => {
    return fetch(`/api/userProfile/GetByIdWithVideos/${id}`)
        .then((res) => res.json());
}