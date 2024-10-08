document.addEventListener('DOMContentLoaded', function () {
    const apiUrl = "https://localhost:7111/urls";

    async function fetchUrls() {
        try {
            const response = await fetch(apiUrl);
            const urls = await response.json();
            displayUrls(urls);
        } catch (error) {
            console.error("Error fetching URLs:", error);
        }
    }

    function displayUrls(urls) {
        const tableBody = document.getElementById("urlTableBody");
        
        urls.sort((a, b) => a.originalUrl.localeCompare(b.originalUrl, undefined, { sensitivity: 'base' }));

        tableBody.innerHTML = ""; 

        urls.forEach(url => {
            const row = document.createElement("tr");

            row.innerHTML = `
                <td>${url.originalUrl}</td>
                <td><a href="${url.shortenedUrl}" target="_blank">${url.shortenedUrl}</a></td>
                <td>${new Date(url.creationDate).toLocaleDateString()}</td>
                <td>${url.transitionCount}</td>
                <td>
                    <button class="btn btn-danger btn-sm delete-btn" data-key="${url.shortenedUrlKey}">Delete</button>
                    <button class="btn btn-warning btn-sm update-btn" data-key="${url.shortenedUrlKey}">Update</button>
                </td>
            `;

            tableBody.appendChild(row);
        });

        const deleteButtons = document.querySelectorAll('.delete-btn');
        deleteButtons.forEach(button => {
            button.addEventListener('click', deleteUrl);
        });

        const updateButtons = document.querySelectorAll('.update-btn');
        updateButtons.forEach(button => {
            button.addEventListener('click', updateUrl);
        });
    }

    async function deleteUrl(event) {
        const shortUrlKey = event.target.dataset.key;
        const deleteUrl = `https://localhost:7111/${shortUrlKey}`;

        const confirmed = confirm('Are you sure you want to delete this URL?');
        if (!confirmed) return;

        try {
            const response = await fetch(deleteUrl, { method: 'DELETE' });
            if (response.ok) {
                fetchUrls();
            } else {
                console.error("Failed to delete URL");
            }
        } catch (error) {
            console.error("Error deleting URL:", error);
        }
    }

    async function updateUrl(event) {
        const shortUrlKey = event.target.dataset.key;
        const updateUrl = `https://localhost:7111/${shortUrlKey}`;
        
        try {
            const response = await fetch(updateUrl, { method: 'PUT' });
            if (response.ok) {
                fetchUrls();
            } else {
                console.error("Failed to update URL");
            }
        } catch (error) {
            console.error("Error updating URL:", error);
        }
    }

    fetchUrls();
});