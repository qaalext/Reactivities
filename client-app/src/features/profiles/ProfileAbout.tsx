import React, {useState} from 'react';
import {useStore} from "../../app/stores/store";
import {Button, Grid, Header, Tab} from "semantic-ui-react";
import ProfileEditForm from "./ProfileEdit";
import { observer } from 'mobx-react-lite';




export default observer(function ProfileAbout(){

    const {profileStore} = useStore();
    const {isCurrentUser, profile} = profileStore;
    const [editMode, setEditMode] = useState(false);
    return (
        <Tab.Pane>
            <Grid>
                <Grid.Column width='16'>
                    <Header floated='left' icon='user' content={`About ${profile?.displayName}`} />
                    {isCurrentUser && (
                        <Button 
                            floated='right'
                            basic
                            content={editMode ? 'Cancel' : 'Edit Profile'}
                            onClick={() => setEditMode(!editMode)}
                        />
                    )}
                </Grid.Column>
                <Grid.Column width='16' >
                    {editMode ? <ProfileEditForm setEditMode={setEditMode} /> :
                    <span style={{whiteSpace: 'pre-wrap'}}>{profile?.bio}</span>
                    //Note: the style ‘whiteSpace: ‘pre-wrap’’ will preserve line breaks that are entered
                    //into the text area here. 
                }
                </Grid.Column>
            </Grid>
        </Tab.Pane>

    )




})