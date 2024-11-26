import React from 'react';
import { Message } from 'semantic-ui-react';

interface Props {
    // errors : string[] | null;
    errors : any;

}

export default function ValidationErrors({errors}: Props){
    return (
        <Message error>
            {errors && (
                <Message.List>
                    {errors.map((err: any, indexOfItem : any) => (
                        <Message.Item key={indexOfItem}> 
                            {err}
                        </Message.Item>
                    ))}
                </Message.List>
            )}
        </Message>
    )

}