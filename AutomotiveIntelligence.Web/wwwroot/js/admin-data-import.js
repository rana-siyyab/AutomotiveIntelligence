document.addEventListener("DOMContentLoaded", function() {

    const fileInput = document.querySelector(".file-input");
    const dropArea = document.querySelector(".file-drop-area");
    const fileName = document.querySelector(".selected-file-name");
    const fileStatus = document.querySelector(".file-status");
    const uploadIcon = document.querySelector(".upload-icon");

    if (!fileInput || !dropArea) {
        return;
    }

    fileInput.addEventListener("change", function() {

        if (!this.files || this.files.length === 0) {
            resetFileDisplay();
            return;
        }

        const file = this.files[0];

        fileName.textContent = file.name;

        fileStatus.textContent =
            "CSV file selected and ready to import";

        uploadIcon.textContent = "✓";

        dropArea.classList.add("file-selected");
    });

    dropArea.addEventListener("dragover", function(event) {

        event.preventDefault();

        dropArea.classList.add("drag-over");
    });

    dropArea.addEventListener("dragleave", function() {

        dropArea.classList.remove("drag-over");
    });

    dropArea.addEventListener("drop", function(event) {

        event.preventDefault();

        dropArea.classList.remove("drag-over");

        if (!event.dataTransfer.files ||
            event.dataTransfer.files.length === 0) {
            return;
        }

        const file = event.dataTransfer.files[0];

        if (!file.name.toLowerCase().endsWith(".csv")) {

            fileName.textContent = "";
            fileStatus.textContent =
                "Please select a CSV file";

            uploadIcon.textContent = "↑";

            dropArea.classList.remove("file-selected");

            return;
        }

        fileInput.files = event.dataTransfer.files;

        fileName.textContent = file.name;

        fileStatus.textContent =
            "CSV file selected and ready to import";

        uploadIcon.textContent = "✓";

        dropArea.classList.add("file-selected");
    });

    function resetFileDisplay() {

        fileName.textContent = "";

        fileStatus.textContent =
            "CSV files only";

        uploadIcon.textContent = "↑";

        dropArea.classList.remove("file-selected");
    }
});