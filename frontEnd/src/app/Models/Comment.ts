export interface Comment{
    id?:number;
    movieId?:number;
    text:string;
    authorName:string;
    createdAt?:string;
    isEditing?:boolean;
}