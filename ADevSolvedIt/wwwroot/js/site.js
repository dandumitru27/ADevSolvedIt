// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function renderMarkdownAndHighlight(input) {
    var md = window.markdownit();

    var renderedHtml = md.render(input);

    return highlightCode(renderedHtml);
}

function highlightCode(htmlMarkup) {
    const preCodeRegex = /<pre><code(?: class="language-(.*)")?>([\s\S]*?)<\/code><\/pre>/g;

    return htmlMarkup.replace(preCodeRegex, function (_match, languageName, codeBlock) {
        let codeBlockHighlighted;

        // decode HTML entities
        const textAreaElement = document.createElement('textarea');
        textAreaElement.innerHTML = codeBlock;
        codeBlock = textAreaElement.value;

        if (languageName && hljs.getLanguage(languageName)) {
            codeBlockHighlighted = hljs.highlight(languageName, codeBlock, true).value;
        }
        else {
            codeBlockHighlighted = hljs.highlightAuto(codeBlock).value;
        }

        return '<pre><code class="hljs">' + codeBlockHighlighted + '</pre></code>';
    });
}
