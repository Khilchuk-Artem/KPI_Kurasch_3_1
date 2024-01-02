export interface BorrowingSummaryDTO {
    borrowingId: string;
    createdDate: string; // Adjust the type based on your DateOnly format
    status: string;
    creator: string;
}
