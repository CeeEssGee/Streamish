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
        .then((res) => res.json())
};

// NOTE This assumes your API is set up to return a video object which includes an array of comments. If you need to make an additional fetch call to get the comments for a video, update the getVideo function as needed.
export const getVideo = (id) => {
    return fetch(`${getWithCommentsUrl}/${id}`).then((res) => res.json());
};