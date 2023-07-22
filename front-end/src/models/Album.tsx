import { RecordLabel } from './RecordLabel';
import { Artist } from './Artist';

export interface Album{
    AlbumId?: number;
    Title: string;
    Genre: string;
    YearOfRelease: string;
    Price: number;
    NumberOfTracks: number;
    ArtistId: number;
    RecordLabelId: number;
    Artist?: Artist;
    RecordLabel?: RecordLabel;
    [key: string]: any;
}