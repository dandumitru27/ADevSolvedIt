window.onload = function () {
    renderProblemPreview();
    renderSolutionPreview();

    const problemMarkdown = document.getElementById('problemMarkdown');
    problemMarkdown.addEventListener('input', renderProblemPreview);

    const solutionMarkdown = document.getElementById('solutionMarkdown');
    solutionMarkdown.addEventListener('input', renderSolutionPreview);
}

function renderProblemPreview() {
    const problemMarkdown = document.getElementById("problemMarkdown");

    const preview = renderMarkdownAndHighlight(problemMarkdown.value);

    document.getElementById("problemPreview").innerHTML = preview;
}

function renderSolutionPreview() {
    const solutionMarkdown = document.getElementById("solutionMarkdown");

    const preview = renderMarkdownAndHighlight(solutionMarkdown.value);

    document.getElementById("solutionPreview").innerHTML = preview;
}