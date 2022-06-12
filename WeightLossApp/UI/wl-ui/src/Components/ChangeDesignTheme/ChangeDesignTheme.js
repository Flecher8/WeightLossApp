import { useState } from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col } from "react-bootstrap";

function ChangeDesignTheme(props) {
    const [validated, setValidated] = useState(false);
    
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

    // Fetch function
	const handleSubmit = event => {
		const form = event.currentTarget;
		if (!form.checkValidity()) {
			event.preventDefault();
			event.stopPropagation();
		} else {
			event.preventDefault();
			fetch(constants.API_URL + "DesignThemeData", {
				method: props.method,
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				},

				body: JSON.stringify(props.designTheme)
			})
				.then(res => res.json())
				.then(
					result => {
						// Update main table
						props.getDesignThemes();

						// Close
						props.state();
					},
					error => {
						alert("Failed");
					}
				);
		}

		setValidated(true);
	};
}