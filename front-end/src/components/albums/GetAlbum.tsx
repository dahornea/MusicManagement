import { Card, CardActions, CardContent, Icon, IconButton,Box, Button } from "@mui/material";
import {Container} from "@mui/system";
import { useEffect, useState } from "react";
import {Link, useParams} from "react-router-dom";
import {BACKEND_API_URL} from "../../constants";
import {Album} from "../../models/Album";
import EditIcon from "@mui/icons-material/Edit";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import * as React from "react";

export const AlbumDetails = () => {
    const {albumId} = useParams();
    const [album, setAlbum] = useState<Album>();

    useEffect(() => {
        const fetchAlbum = async () => {
            const response = await fetch(`${BACKEND_API_URL}/Album/${albumId}`);
            const album = await response.json();
            setAlbum(album);
        };
        fetchAlbum();
    }, [albumId]);

    return (
        <Container>
            <Card sx={{p:2}}>
                <CardContent>
                    <Box display = "flex" alignItems = "flex-start">
                        <IconButton
                            component = {Link}
                            sx = {{mb: 2, mr:3}}
                            to ={'/albums'}
                        >
                            <ArrowBackIcon/>
                        </IconButton>
                        <h1
                            style={{
                                flex: 1,
                                textAlign: "center",
                                marginLeft: -64,
                                marginTop: -4,
                            }}
                            >
                                Album details
                            </h1>
                    </Box>

                    <Box sx={{ml:1}}>
                        <p>Title: {album?.Title}</p>
                        <p>Genre: {album?.Genre}</p>
                        <p>Year of Release: {album?.YearOfRelease}</p>
                        <p>Price: {album?.Price}</p>
                        <p>Number of Tracks: {album?.NumberOfTracks}</p>
                        <p>Artist:{album?.Artist?.Name}</p>
                        <p>Record Label: {album?.RecordLabel?.Name}</p> 
                    </Box>
                    </CardContent>
                    <CardActions sx={{mb:1, ml:1, mt:1}}>
                        <Button
                            component = {Link}
                            to = {'/albums/${albumId}/update'}
                            variant = "text"
                            size="large"
                            sx={{color: "gray",
                        textTransform: "none",}}
                        startIcon={<EditIcon/>}
                        >
                            Edit
                        </Button>

                        <Button
                            component = {Link}
                            to = {'/albums/${albumId}/delete'}
                            variant = "text"
                            size="large"
                            sx={{color:"red", textTransform: "none"}}
                            startIcon={<DeleteForeverIcon/>}
                        >
                            Delete
                        </Button>

                    </CardActions>
            </Card>
        </Container>
        );
};