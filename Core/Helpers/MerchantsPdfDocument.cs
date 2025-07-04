using Core.Entities;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class MerchantsPdfDocument : IDocument
{
    private readonly IEnumerable<Merchant> _merchants;

    public MerchantsPdfDocument(IEnumerable<Merchant> merchants)
    {
        _merchants = merchants;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(20);
            page.Size(PageSizes.A4);

            page.Header()
                .Text("Merchants Report")
                .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

            page.Content()
                .Table(table =>
                {
                    // Define columns
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(1);
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Id");
                        header.Cell().Element(CellStyle).Text("Name");
                        header.Cell().Element(CellStyle).Text("Industry");
                        header.Cell().Element(CellStyle).Text("Location");
                        header.Cell().Element(CellStyle).Text("Tenure");
                        header.Cell().Element(CellStyle).Text("TenureInDays");
                        header.Cell().Element(CellStyle).Text("PhoneNo");
                        header.Cell().Element(CellStyle).Text("LastSurvey");
                        header.Cell().Element(CellStyle).Text("LastFeedback");
                        header.Cell().Element(CellStyle).Text("Ledger");
                        header.Cell().Element(CellStyle).Text("LastTransaction");
                        header.Cell().Element(CellStyle).Text("LastTicket");
                        header.Cell().Element(CellStyle).Text("CreatedOn");
                        header.Cell().Element(CellStyle).Text("LastEscalation");

                        static IContainer CellStyle(IContainer container) =>
                            container.DefaultTextStyle(x => x.SemiBold()).Padding(2).Background(Colors.Grey.Lighten2);
                    });

                    // Rows
                    foreach (var m in _merchants)
                    {
                        table.Cell().Element(CellStyle).Text(m.Id.ToString());
                        table.Cell().Element(CellStyle).Text(m.Name);
                        table.Cell().Element(CellStyle).Text(m.Industry);
                        table.Cell().Element(CellStyle).Text(m.Location);
                        table.Cell().Element(CellStyle).Text(m.Tenure);
                        table.Cell().Element(CellStyle).Text(m.TenureInDays.ToString());
                        table.Cell().Element(CellStyle).Text(m.PhoneNo);
                        table.Cell().Element(CellStyle).Text(m.LastSurvey?.ToString());
                        table.Cell().Element(CellStyle).Text(m.LastFeedback?.ToString());
                        table.Cell().Element(CellStyle).Text(m.Ledger?.ToString());
                        table.Cell().Element(CellStyle).Text(m.LastTransaction?.ToString());
                        table.Cell().Element(CellStyle).Text(m.LastTicket?.ToString());
                        table.Cell().Element(CellStyle).Text(m.CreatedOn.ToString());
                        table.Cell().Element(CellStyle).Text(m.LastEscalation?.ToString());
           
                    
                    static IContainer CellStyle(IContainer container) =>
                            container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(2);
                    }
                });
        });
    }
}
