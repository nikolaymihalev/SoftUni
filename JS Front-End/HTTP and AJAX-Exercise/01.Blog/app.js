function attachEvents() {
    const allPosts = [];

    const BASE_URL = "http://localhost:3030/jsonstore/blog";
    const POSTS_URL = `${BASE_URL}/posts`;
    const COMMENTS_URL = `${BASE_URL}/comments`;

    const loadPostsEl = document.getElementById('btnLoadPosts');
    const selectElement = document.getElementById('posts');
    const viewPostCommentElement = document.getElementById('btnViewPost');
    const postBodyEl = document.getElementById('post-body');
    const postTitleEl = document.getElementById('post-title');
    const postCommentsEl = document.getElementById('post-comments');

    async function customFetch(url) {
        return fetch(url).then((res) => res.json());
    }

    function appendPosts(postsResponse) {
        selectElement.textContent = "";

        Object.values(postsResponse).forEach(({ body, id, title }) => {
            const optionEl = document.createElement('option');

            optionEl.textContent = title;
            optionEl.value = id;

            selectElement.appendChild(optionEl);

            allPosts.push({ body, id, title });
        });
    }

    function fetchAllPosts() {
        customFetch(POSTS_URL).then(appendPosts);
    }

    function fetchSinglePost() {
        const { value: selectedPostId } = selectElement;

        if (!selectedPostId)
            return;

        const selectedPost = allPosts.find((x)=>x.id===selectedPostId);
        postBodyEl.textContent = selectedPost.body;
        postTitleEl.textContent = selectedPost.title;            

        customFetch(`${COMMENTS_URL}`).then((commentsResponse) => {
            postCommentsEl.textContent = "";

            Object.values(commentsResponse).filter(({ postId }) => postId === selectedPostId)
            .forEach(({ id, text }) => {
                const li = document.createElement('li');

                li.textContent = text;
                li.setAttribute("data-id", id);

                postCommentsEl.appendChild(li);
            })
        })
    }

    loadPostsEl.addEventListener('click', fetchAllPosts);
    viewPostCommentElement.addEventListener('click', fetchSinglePost);
}

attachEvents();