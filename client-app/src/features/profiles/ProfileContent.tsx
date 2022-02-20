import { observer } from "mobx-react-lite";
import React from "react";
import { Tab } from "semantic-ui-react";
import { Profile } from "../../app/models/profile";
import { useStore } from "../../app/stores/store";
import ProfileAbout from "./ProfileAbout";
import ProfileFollwiings from "./ProfileFollwiings";
import ProfilePhotos from "./ProfilePhotos";
import ProfileActivities from "./‘ProfileActivities’";

interface Props{
    profile: Profile;

}

export default observer(function ProfileContent({profile} : Props){
    const {profileStore} = useStore();

    const panes = [
            {menuItem: 'About', render: () => <ProfileAbout />},
            {menuItem: 'Photos', render: () => <ProfilePhotos profile={profile}/>},
            {menuItem: 'Events', render: () => <ProfileActivities />},
            {menuItem: 'Followers', render: () => <ProfileFollwiings />},
            {menuItem: 'Following', render: () => <ProfileFollwiings />},

    ];
    return (
        <Tab 
            menu={{fluid: true, vertical: true}}
            menuPosition='right'
            panes={panes}
            onTabChange={(e, data) => profileStore.setActiveTab(data.activeIndex)}
        />
    )



})