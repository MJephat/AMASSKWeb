window.downloadVisitsPdf = async (sponsorName, sponsorLogo, visits) => {

    const { jsPDF } = window.jspdf;

    const doc = new jsPDF();

    // =========================
    // REPORT DATE
    // =========================
    const generatedDate =
        new Date().toLocaleString();

    // =========================
    // LOGO
    // =========================
    try {

        if (sponsorLogo) {

            const response = await fetch(sponsorLogo);

            const blob = await response.blob();

            const reader = new FileReader();

            reader.readAsDataURL(blob);

            reader.onloadend = function () {

                const base64data = reader.result;

                buildPdf(base64data);
            };
        }
        else {
            buildPdf(null);
        }
    }
    catch {
        buildPdf(null);
    }

    // =========================
    // BUILD PDF
    // =========================
    function buildPdf(logoBase64) {

        // Logo
        if (logoBase64) {
            doc.addImage(
                logoBase64,
                'PNG',
                14,
                10,
                30,
                30
            );
        }

        // Title
        doc.setFontSize(20);

        doc.setTextColor(40);

        doc.text(
            `${sponsorName} Visits Report`,
            50,
            22
        );

        // Generated Date
        doc.setFontSize(10);

        doc.setTextColor(100);

        doc.text(
            `Generated: ${generatedDate}`,
            50,
            30
        );

        // Total
        doc.setFontSize(11);

        doc.text(
            `Total Attendees: ${visits.length}`,
            14,
            50
        );

        // =========================
        // TABLE
        // =========================
        const rows = visits.map(v => [
            v.visitorName,
            v.visitorEmail,
            new Date(v.visitDateTime).toLocaleString()
        ]);

        doc.autoTable({
            startY: 60,

            head: [[
                "Visitor Name",
                "Email",
                "Visit Date"
            ]],

            body: rows,

            theme: 'grid',

            headStyles: {
                fillColor: [205, 127, 50], // bronze
                textColor: 255,
                fontStyle: 'bold'
            },

            styles: {
                fontSize: 10,
                cellPadding: 4
            },

            alternateRowStyles: {
                fillColor: [245, 245, 245]
            }
        });

        // Footer
        const pageCount =
            doc.internal.getNumberOfPages();

        for (let i = 1; i <= pageCount; i++) {

            doc.setPage(i);

            doc.setFontSize(9);

            doc.setTextColor(150);

            doc.text(
                `Page ${i} of ${pageCount}`,
                170,
                290
            );
        }

        // Save
        doc.save(
            `${sponsorName}-visits-report.pdf`
        );
    }
};