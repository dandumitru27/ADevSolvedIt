window.onload = function () {
    document.getElementById('btnThanks').onclick = onThanksClick;

    const captchaErrorMessage = document.getElementById('captchaErrorMessage');
    if (captchaErrorMessage) {
        captchaErrorMessage.scrollIntoView();

        renderCommentTextPreview();
    }

    const commentTextMarkdown = document.getElementById('commentTextMarkdown');

    commentTextMarkdown.addEventListener('input', renderCommentTextPreview);
}

function renderCommentTextPreview() {
    const commentTextMarkdown = document.getElementById("commentTextMarkdown");

    const preview = renderMarkdownAndHighlight(commentTextMarkdown.value);

    document.getElementById("commentTextPreview").innerHTML = preview;
}

async function onThanksClick(event) {
    event.target.classList.add("spinner-border");

    sendThanks()
        .then(thanksCount => {
            divThanksCount = document.getElementById("divThanksCount");
            divThanksCount.innerHTML = thanksCount + " Thanks received";
            divThanksCount.style.display = 'block';

            event.target.innerText = "Thanked";
            event.target.disabled = true;
            event.target.classList.remove("spinner-border");
        })
        .catch(() => {
            alert("An error occured. Sorry. I'll try to fix it.");
        });
}

async function sendThanks() {
    const response = await fetch('?handler=AddThanks', {
        method: 'POST',
        headers: {
            "XSRF-TOKEN": document.querySelector("[name='__RequestVerificationToken']").value
        }
    })

    return response.json()
}
