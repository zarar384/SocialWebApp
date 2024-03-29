export interface Message {
  id: number;
  senderId: number;
  senderUsername: string;
  senderPhotoUrl: string;
  recipientId: number;
  recipientUsername: string;
  recipienPhotoUrl: string;
  content: string;
  dateRead?: Date;
  messageSent: Date;
}
