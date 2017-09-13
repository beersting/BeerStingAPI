/**
 * 
 	  @author BeerSting <br>
 	     * <b>The MIT License (MIT) Copyright: </b><br>
		 * Copyright (c) 2017, BeerSting<br>
		 * 
		 * <b>Create by: </b><br>
		 * Yoottapong Wongwiwut<br>  
		 * 
		 * <b>Create Date: </b><br>
		 *  July 07 2017<br>
		 * 
		 * <b>Email: </b><br>
		 * <A href="mailto:beer.sting@gmail.com">beer.sting@gmail.com</A><br> 
	  @version 1.0
 * 
 */

namespace BeerSting.Api.Enums
{

    public enum FtpCommand
    {
        USER,
        GET, // Download
        GETLISTFILES,
        UPLOAD,
        TM1PROCESS,
        SERVER,
        CLIENT,
        GET_OK,
        GET_FAILED,
        BEGINSEND,
        OK,
        SUCCESS,
        CLIENT_DISCONNECTING,
        CLIENT_CLOSE,
        SERVER_DISCONNECT,
        ERROR
    }
}