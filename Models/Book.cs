using System;

namespace mySLMS_OOP_System.Models
{
    public enum BookStatus
    {
        Available,
        Borrowed,
        Reserved
    }

    public class Book
    {
        // Encapsulation: state is controlled (private set)
        public int Id { get; }
        public string Title { get; }
        public string Author { get; }

        public BookStatus Status { get; private set; } = BookStatus.Available;

        public int? BorrowerId { get; private set; }
        public int? DueDay { get; private set; }

        // Reservation: allowed only when borrowed. Pickup expires after return + 3 days.
        public int? ReservedById { get; private set; }
        public int? PickupUntilDay { get; private set; }

        public Book(int id, string title, string author)
        {
            Id = id;
            Title = title.Trim();
            Author = author.Trim();
        }

        public bool IsOverdue(int currentDay) =>
            Status == BookStatus.Borrowed && DueDay.HasValue && currentDay > DueDay.Value;

        public bool CanBeBorrowedBy(int memberId)
        {
            if (Status == BookStatus.Available) return true;
            if (Status == BookStatus.Reserved && ReservedById == memberId) return true;
            return false;
        }

        public bool Borrow(int memberId, int currentDay)
        {
            if (!CanBeBorrowedBy(memberId)) return false;

            // If reserved for this member, clear reservation on borrow
            if (Status == BookStatus.Reserved && ReservedById == memberId)
                ClearReservation();

            Status = BookStatus.Borrowed;
            BorrowerId = memberId;
            DueDay = currentDay + 14; // due date rule
            return true;
        }

        public bool Return(int memberId, int currentDay)
        {
            if (Status != BookStatus.Borrowed) return false;
            if (BorrowerId != memberId) return false;

            BorrowerId = null;
            DueDay = null;

            // If someone reserved it while borrowed, it becomes reserved-for-pickup
            if (ReservedById.HasValue)
            {
                Status = BookStatus.Reserved;
                PickupUntilDay = currentDay + 3; // reservation expiry rule
            }
            else
            {
                Status = BookStatus.Available;
            }

            return true;
        }

        public bool Reserve(int memberId)
        {
            // Case study rule: reserve only if currently borrowed
            if (Status != BookStatus.Borrowed) return false;

            // One reservation at a time (simple and demo-friendly)
            if (ReservedById.HasValue) return false;

            ReservedById = memberId;
            PickupUntilDay = null; // pickup window starts on return
            return true;
        }

        public void ExpireReservationIfNeeded(int currentDay)
        {
            if (Status == BookStatus.Reserved && PickupUntilDay.HasValue && currentDay > PickupUntilDay.Value)
            {
                ClearReservation();
                Status = BookStatus.Available;
            }
        }

        private void ClearReservation()
        {
            ReservedById = null;
            PickupUntilDay = null;
        }

        public string DisplayLine(int currentDay)
        {
            string s = $"[{Id}] {Title} — {Author} | {Status}";

            if (Status == BookStatus.Borrowed && BorrowerId.HasValue && DueDay.HasValue)
            {
                s += $" | Borrower={BorrowerId} DueDay={DueDay}";
                if (IsOverdue(currentDay)) s += " (OVERDUE)";
            }

            if (ReservedById.HasValue)
            {
                s += $" | ReservedBy={ReservedById}";
                if (Status == BookStatus.Reserved && PickupUntilDay.HasValue)
                    s += $" PickupUntil={PickupUntilDay}";
            }

            return s;
        }
    }
}