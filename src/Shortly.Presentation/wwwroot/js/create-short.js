document.addEventListener('DOMContentLoaded', function () {
    const createForm = document.getElementById("createForm");
    const baseUrl = "https://localhost:7111";

    createForm.addEventListener('submit', async function (event) {
        event.preventDefault();

        const originalUrl = document.getElementById("originalUrl").value;

        try {
            const response = await fetch(baseUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ url: originalUrl })
            });

            if (response.ok) {
                alert("Short URL created successfully!");
                window.location.href = "index.html";
            } else {
                console.error("Failed to create short URL");
            }
        } catch (error) {
            console.error("Error creating short URL:", error);
        }
    });
});