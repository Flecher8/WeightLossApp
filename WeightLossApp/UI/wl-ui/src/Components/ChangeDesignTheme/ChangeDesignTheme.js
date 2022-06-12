import { useState } from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col } from "react-bootstrap";

function ChangeDesignTheme(props) {
    // Save img in imgur and write img to object
	function saveImg(ev) {
		const formdata = new FormData();
		formdata.append("image", ev.target.files[0]);
		fetch(constants.API_Imgur, {
			method: "post",
			headers: {
				Authorization: "Client-ID " + constants.Client_ID,
				Accept: "application/json"
			},
			body: formdata
		})
			.then(res => res.json())
			.then(data => {
				props.setDesignTheme({
					Id: props.designTheme.Id,
					BaseColor: props.designTheme.BaseColor,
					SecondaryColor: props.designTheme.SecondaryColor,
					AccentColor: props.designTheme.AccentColor,
					IconImage: data.data.link,
				});
			});
	}
}