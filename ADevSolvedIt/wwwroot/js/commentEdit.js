window.onload = function () {
    renderCommentTextPreview();

    const commentTextMarkdown = document.getElementById('commentTextMarkdown');

    commentTextMarkdown.addEventListener('input', renderCommentTextPreview);
}

function renderCommentTextPreview() {
    const commentTextMarkdown = document.getElementById("commentTextMarkdown");

    const preview = renderMarkdownAndHighlight(commentTextMarkdown.value);

    document.getElementById("commentTextPreview").innerHTML = preview;
}
